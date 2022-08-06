using IEduZimAPI.CoreClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface IFileRepository
    {
        Task<Result<UploadFile>> AddAsync(UploadFile imageFile);
        Task<Result<IEnumerable<UploadFile>>> GetAllAsync();
    }
}