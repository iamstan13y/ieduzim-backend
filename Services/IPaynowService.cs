using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Enums;
using IEduZimAPI.Models.Local;
using System.Threading.Tasks;

namespace IEduZimAPI.Services
{
    public interface IPaynowService
    {
        Task<Result<PaynowResponse>> CreatePaymentAsync(PaynowPaymentRequest request);
        Task<Result<PaynowStatus>> CheckPaymentStatusAsync(string pollUrl);
    }
}