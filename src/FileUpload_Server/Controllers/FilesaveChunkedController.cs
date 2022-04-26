using System.Net;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class FilesaveChunkedController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<FilesaveController> _logger;

    public FilesaveChunkedController(IWebHostEnvironment env, ILogger<FilesaveController> logger)
    {
        this._env = env;
        this._logger = logger;
    }

    [HttpPost]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> PostFile(IFormFile uploadFile)
    {
        if (uploadFile == null || uploadFile.Length == 0) return BadRequest();

        // Temporary, does NOT work when published, use only for local debugging
        var uploadFolderPath = Path.Combine(_env.ContentRootPath, "unsafe_uploads");

        if (Directory.Exists(uploadFolderPath) == false)
        {
            Directory.CreateDirectory(uploadFolderPath);
        }

        string fullFilePath = Path.Combine(uploadFolderPath, uploadFile.FileName);

        using (var fs = new FileStream(fullFilePath, FileMode.Append))
            await uploadFile.CopyToAsync(fs);

        return Created(fullFilePath, null);
    }

}