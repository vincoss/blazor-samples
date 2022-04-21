using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Net;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]")]
public class FilesaveLargeController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<FilesaveController> _logger;

    public FilesaveLargeController(IWebHostEnvironment env, ILogger<FilesaveController> logger)
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

        var request = HttpContext.Request;

        // validation of Content-Type
        // 1. first, it must be a form-data request
        // 2. a boundary should be found in the Content-Type
        if (!request.HasFormContentType ||
            !MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) ||
            string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
        {
            return new UnsupportedMediaTypeResult();
        }

        var reader = new MultipartReader(mediaTypeHeader.Boundary.Value, request.Body);
        var section = await reader.ReadNextSectionAsync();

        // This sample try to get the first file from request and save it
        // Make changes according to your needs in actual use
        while (section != null)
        {
            var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition,
                out var contentDisposition);

            if (hasContentDispositionHeader && contentDisposition.DispositionType.Equals("form-data") &&
                !string.IsNullOrEmpty(contentDisposition.FileName.Value))
            {
                // Don't trust any file name, file extension, and file data from the request unless you trust them completely
                // Otherwise, it is very likely to cause problems such as virus uploading, disk filling, etc
                // In short, it is necessary to restrict and verify the upload
                // Here, we just use the temporary folder and a random file name

                // Get the temporary folder, and combine a random file name with it
                var fileName = Path.GetRandomFileName();
                var saveToPath = Path.Combine(Path.GetTempPath(), fileName);

                using (var targetStream = System.IO.File.Create(saveToPath))
                {
                    await section.Body.CopyToAsync(targetStream);
                }

                return Ok();
            }

            section = await reader.ReadNextSectionAsync();
        }

        // If the code runs to this location, it means that no files have been saved
        return BadRequest("No files data in the request.");


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

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var factories = context.ValueProviderFactories;
            factories.RemoveType<FormValueProviderFactory>();
            factories.RemoveType<JQueryFormValueProviderFactory>();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}