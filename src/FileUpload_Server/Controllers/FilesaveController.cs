using System.Net;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class FilesaveController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<FilesaveController> _logger;

    public FilesaveController(IWebHostEnvironment env, ILogger<FilesaveController> logger)
    {
        this._env = env;
        this._logger = logger;
    }

    [HttpPost]
    [RequestSizeLimit(4294967296)]
    [RequestFormLimits(MultipartBodyLengthLimit = 4294967296)]
    public async Task<ActionResult<IList<UploadResult>>> PostFile([FromForm] IEnumerable<IFormFile> files)
    {
        var maxAllowedFiles = 30;
        long maxFileSize = 1024 * 1024 * 1500;
        var filesProcessed = 0;
        var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
        List<UploadResult> uploadResults = new();

        _logger.LogDebug($"Begin file upload. Count: {files.Count()}");

        foreach (var file in files)
        {
            _logger.LogDebug($"Save file: {file.FileName}");

            var uploadResult = new UploadResult();
            string trustedFileNameForFileStorage;
            var untrustedFileName = file.FileName;
            uploadResult.FileName = untrustedFileName;
            var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);

            if (filesProcessed < maxAllowedFiles)
            {
                if (file.Length == 0)
                {
                    _logger.LogInformation("{FileName} length is 0 (Err: 1)", trustedFileNameForDisplay);
                    uploadResult.ErrorMessage = "File length is 0.";
                }
                else if (file.Length > maxFileSize)
                {
                    _logger.LogInformation("{FileName} of {Length} bytes is " +
                        "larger than the limit of {Limit} bytes (Err: 2)",
                        trustedFileNameForDisplay, file.Length, maxFileSize);
                    uploadResult.ErrorMessage = $"File is larger then larger than the limit of {maxFileSize} bytes.";
                }
                else
                {
                    try
                    {
                        trustedFileNameForFileStorage = Path.GetRandomFileName();
                        var uploadFolderPath = Path.Combine(_env.ContentRootPath, "unsafe_uploads");

                        if(Directory.Exists(uploadFolderPath) == false)
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        var uploadFilePath = Path.Combine(uploadFolderPath, trustedFileNameForDisplay);

                        await using FileStream fs = new(uploadFilePath, FileMode.Create);
                        await file.CopyToAsync(fs);

                        _logger.LogInformation("{FileName} saved at {Path}", trustedFileNameForDisplay, uploadFilePath);
                        uploadResult.Uploaded = true;
                        uploadResult.StoredFileName = trustedFileNameForFileStorage;
                    }
                    catch (IOException ex)
                    {
                        _logger.LogError("{FileName} error on upload (Err: 3): {Message}",
                            trustedFileNameForDisplay, ex.Message);
                        uploadResult.ErrorMessage = ex.Message;
                    }
                }

                filesProcessed++;
            }
            else
            {
                _logger.LogInformation("{FileName} not uploaded because the " +
                    "request exceeded the allowed {Count} of files (Err: 4)",
                    trustedFileNameForDisplay, maxAllowedFiles);
                uploadResult.ErrorMessage = $"File not uploaded because the request exceeded the allowed {maxAllowedFiles} of files";
            }

            uploadResults.Add(uploadResult);
        }

        return new CreatedResult(resourcePath, uploadResults);
    }

    public class UploadResult
    {
        public bool Uploaded { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ErrorMessage { get; set; }
    }
}