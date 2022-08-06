using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IEduZimAPI.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly IFileRepository _fileRepository;

        public FileService(IConfiguration configuration, IFileRepository fileRepository)
        {
            _configuration = configuration;
            _fileRepository = fileRepository;
        }

        public async Task<Result<UploadFile>> UploadFileAsync(IFormFile file)
        {
            try
            {
                string basePath = Path.Combine(Directory.GetCurrentDirectory() + "/Files/");
                string fileName = Path.GetFileName(file.FileName);
                string newFileName = string.Concat($"iez-{DateTime.Now.Ticks.ToString()[12..]}-", fileName);
                string filePath = string.Concat($"{basePath}", newFileName);

                string url = $"{_configuration["Urls:DevBaseUrl"]}/Files/{newFileName}";
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var result = await _fileRepository.AddAsync(new UploadFile
                {
                    FileName = newFileName,
                    Url = url,
                    Path = filePath
                });

                return result;
            }
            catch (Exception ex)
            {
                return new Result<UploadFile>(false, ex.ToString(), null);
            }
        }
    }
}