using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Local;
using System.Threading.Tasks;

namespace IEduZimAPI.Services
{
    public interface IPaynowService
    {
        Task<Result<PaynowResponse>> CreatePaymentAsync(PaynowPaymentRequest request);
        Task<string> CheckPaymentStatusAsync(string pollUrl);
    }
}