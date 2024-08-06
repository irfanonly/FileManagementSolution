using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileManagement.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        
        [HttpGet("getfile")]
        public IActionResult GetFile()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SampleFiles", "example.txt");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", "example.txt");
        }
    }
}
