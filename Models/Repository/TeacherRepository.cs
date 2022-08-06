using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;

        public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Teacher>> AddAsync(Teacher teacher)
        {
            try
            {
                await _context.Teachers!.AddAsync(teacher);

                await _context.SaveChangesAsync();

                return new Result<Teacher>(teacher);
            }
            catch (Exception)
            {
                return new Result<Teacher>(false, "Failed to add Teacher.", null);
            }
        }
    }
}