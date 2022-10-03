using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blazor_HostAspNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public OneController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            var file = Path.Combine(AppContext.BaseDirectory, @"one\index.html");
         //   var file = _env.ContentRootFileProvider.GetFileInfo("blazorone/index.html");
            return PhysicalFile(file, "text/html");
        }
    }
}
