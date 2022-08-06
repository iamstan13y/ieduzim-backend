using IEduZimAPI.CoreClasses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context) => _context = context;
      
        public async Task<Result<UploadFile>> AddAsync(UploadFile imageFile)
        {
            await _context.UploadFiles!.AddAsync(imageFile);
            await _context.SaveChangesAsync();

            return new Result<UploadFile>(imageFile);
        }

        public async Task<Result<IEnumerable<UploadFile>>> GetAllAsync()
        {
            var files = await _context.UploadFiles!.ToListAsync();
            return new Result<IEnumerable<UploadFile>>(files);
        }
    }
}