using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ISubscriptionRepository
    {
        Task<Result<Subscription>> AddAsync();
    }
}