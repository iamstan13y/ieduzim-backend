using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface IPaymentRepository
    {
        Task<Result<Payment>> AddAsync(Payment payment);
        Task<Result<Payment>> GetStatusAsync(string refNumber);
    }
}