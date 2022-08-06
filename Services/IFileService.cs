using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IEduZimAPI.Services
{
    public interface IFileService
    {
        Task<Result<UploadFile>> UploadFileAsync(IFormFile formFile);
    }
}