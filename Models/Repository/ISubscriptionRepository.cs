using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ISubscriptionRepository
    {
        Task<Result<Subscription>> AddAsync(Subscription subscription);
        Task<Result<IEnumerable<Subscription>>> GetAllAsync();
        Task<Result<Subscription>> GetByIdAsync(int id);
        Task<Result<IEnumerable<Subscription>>> GetByStudentIdAsync(int studentId);
        Task<Result<IEnumerable<Subscription>>> GetByTeacherIdAsync(int teacherId);
    }
}