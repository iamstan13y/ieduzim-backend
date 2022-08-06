using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Repository;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FileServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;

        public FilesController(IFileService fileService, IFileRepository fileRepository)
        {
            _fileService = fileService;
            _fileRepository = fileRepository;
        }

        [HttpPost("upload-file")]
        [Authorize]
        [ProducesResponseType(typeof(Result<UploadFile>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<UploadFile>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            var result = await _fileService.UploadFileAsync(file);

            if (!result.Succeeded) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _fileRepository.GetAllAsync());
    }
}