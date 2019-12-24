using EatCode.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EatCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var result = await fileService.DownloadAsyncAsStreamById("5d9a4349750d6d40a4dbd782");
            if (result == null)
            { return Conflict(); }
            return Ok(result);
        }

        [HttpGet("get-by-file-name/{fileName}")]
        public async Task<IActionResult> GetByFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            { return BadRequest(); }
            var result = await fileService.DownloadAsyncAsStreamByName(fileName);
            if (result == null)
            { return Conflict(); }
            return Ok(result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            { return BadRequest(); }
            var result = await fileService.DownloadAsyncAsStreamById(id);
            if (result == null)
            { return Conflict(); }
            return Ok(result);
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> Upload([FromForm]IFormFile file)
        {
            if (file != null)
            {
                if (file.ContentType.Contains("image"))
                { return BadRequest("Sorry only image jpg/jpeg/png accepted"); }

                if (file.Length >= (300 * 1024))
                { return BadRequest($"Sorry {file.FileName} is exceeds  300kb"); }

                var result = await fileService.Upload(file);
                if (string.IsNullOrWhiteSpace(result))
                { return Conflict(); }
                return Ok(result);
            }
            return NoContent();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm]IFormFile file)
        {
            if (file == null)
            { return NoContent(); }
            var result = await fileService.Upload(file);
            if (string.IsNullOrWhiteSpace(result))
            { return Conflict(); }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            { return BadRequest(); }
            var result = await fileService.DeleteFile(id);
            if (result)
            { return Ok(); }
            return NoContent();
        }
    }
}