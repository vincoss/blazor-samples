using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]")]
public class FilesaveLargeOtherController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<FilesaveController> _logger;

    public FilesaveLargeOtherController(IWebHostEnvironment env, ILogger<FilesaveController> logger)
    {
        this._env = env;
        this._logger = logger;
    }

    [HttpPost]
	[DisableFormValueModelBinding]
	public async Task<ActionResult<IList<UploadResult>>> PostFile()
    {
        var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
        List<UploadResult> uploadResults = new();

		// full path to file in temp location
		var filePath = Path.GetTempFileName();
		var uploadFolderPath = Path.Combine(_env.ContentRootPath, "unsafe_uploads");
		var uploadFilePath = Path.Combine(uploadFolderPath, DateTime.Now.Ticks.ToString());

		using (var stream = new FileStream(uploadFilePath, FileMode.Create))
		{
			await Request.StreamFile(stream);
		}

		// process uploaded files
		// Don't rely on or trust the FileName property without validation.

		return new CreatedResult(resourcePath, uploadResults);

        //// If the code runs to this location, it means that no files have been saved
        //return BadRequest("No files data in the request.");
    }

    public class UploadResult
    {
        public bool Uploaded { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ErrorMessage { get; set; }
    }
}


public static class FileStreamingHelper
{
	private static readonly FormOptions _defaultFormOptions = new FormOptions();

	public static async Task<FormValueProvider> StreamFile(this HttpRequest request, Stream targetStream)
	{
		if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
		{
			throw new Exception($"Expected a multipart request, but got {request.ContentType}");
		}

		// Used to accumulate all the form url encoded key value pairs in the 
		// request.
		var formAccumulator = new KeyValueAccumulator();
		string targetFilePath = null;

		var boundary = MultipartRequestHelper.GetBoundary(
			MediaTypeHeaderValue.Parse(request.ContentType),
			_defaultFormOptions.MultipartBoundaryLengthLimit);
		var reader = new MultipartReader(boundary, request.Body);

		var section = await reader.ReadNextSectionAsync();
		while (section != null)
		{
			ContentDispositionHeaderValue contentDisposition;
			var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

			if (hasContentDispositionHeader)
			{
				if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
				{
					await section.Body.CopyToAsync(targetStream);
				}
				else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
				{
					// Content-Disposition: form-data; name="key"
					//
					// value

					// Do not limit the key name length here because the 
					// multipart headers length limit is already in effect.
					var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
					var encoding = GetEncoding(section);
					using (var streamReader = new StreamReader(
						section.Body,
						encoding,
						detectEncodingFromByteOrderMarks: true,
						bufferSize: 1024,
						leaveOpen: true))
					{
						// The value length limit is enforced by MultipartBodyLengthLimit
						var value = await streamReader.ReadToEndAsync();
						if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
						{
							value = String.Empty;
						}
						formAccumulator.Append(key.Value, value); // For .NET Core <2.0 remove ".Value" from key

						if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
						{
							throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
						}
					}
				}
			}

			// Drains any remaining section body that has not been consumed and
			// reads the headers for the next section.
			section = await reader.ReadNextSectionAsync();
		}

		// Bind form data to a model
		var formValueProvider = new FormValueProvider(
			BindingSource.Form,
			new FormCollection(formAccumulator.GetResults()),
			CultureInfo.CurrentCulture);

		return formValueProvider;
	}

	private static Encoding GetEncoding(MultipartSection section)
	{
		MediaTypeHeaderValue mediaType;
		var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
		// UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
		// most cases.
		if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
		{
			return Encoding.UTF8;
		}
		return mediaType.Encoding;
	}
}

public static class MultipartRequestHelper
{
	// Content-Type: multipart/form-data; boundary="----WebKitFormBoundarymx2fSWqWSd0OxQqq"
	// The spec says 70 characters is a reasonable limit.
	public static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
	{
		//var boundary = Microsoft.Net.Http.Headers.HeaderUtilities.RemoveQuotes(contentType.Boundary);// .NET Core <2.0
		var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value; //.NET Core 2.0
		if (string.IsNullOrWhiteSpace(boundary))
		{
			throw new InvalidDataException("Missing content-type boundary.");
		}

		if (boundary.Length > lengthLimit)
		{
			throw new InvalidDataException(
				$"Multipart boundary length limit {lengthLimit} exceeded.");
		}

		return boundary;
	}

	public static bool IsMultipartContentType(string contentType)
	{
		return !string.IsNullOrEmpty(contentType)
				&& contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
	}

	public static bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
	{
		// Content-Disposition: form-data; name="key";
		return contentDisposition != null
				&& contentDisposition.DispositionType.Equals("form-data")
				&& string.IsNullOrEmpty(contentDisposition.FileName.Value) // For .NET Core <2.0 remove ".Value"
				&& string.IsNullOrEmpty(contentDisposition.FileNameStar.Value); // For .NET Core <2.0 remove ".Value"
	}

	public static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
	{
		// Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
		return contentDisposition != null
				&& contentDisposition.DispositionType.Equals("form-data")
				&& (!string.IsNullOrEmpty(contentDisposition.FileName.Value) // For .NET Core <2.0 remove ".Value"
					|| !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value)); // For .NET Core <2.0 remove ".Value"
	}
}

public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
{
	public void OnResourceExecuting(ResourceExecutingContext context)
	{
		var formValueProviderFactory = context.ValueProviderFactories
			.OfType<FormValueProviderFactory>()
			.FirstOrDefault();
		if (formValueProviderFactory != null)
		{
			context.ValueProviderFactories.Remove(formValueProviderFactory);
		}

		var jqueryFormValueProviderFactory = context.ValueProviderFactories
			.OfType<JQueryFormValueProviderFactory>()
			.FirstOrDefault();
		if (jqueryFormValueProviderFactory != null)
		{
			context.ValueProviderFactories.Remove(jqueryFormValueProviderFactory);
		}
	}

	public void OnResourceExecuted(ResourceExecutedContext context)
	{
	}
}