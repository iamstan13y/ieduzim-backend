using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface IPaymentRepository
    {
        Task<Result<Payment>> AddAsync(Payment payment);
        Task<Result<PaymentStatusResponse>> GetStatusAsync(string refNumber);
    }
}