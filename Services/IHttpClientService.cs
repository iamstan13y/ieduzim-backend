using System.Threading.Tasks;

namespace IEduZimAPI.Services
{
    public interface IHttpClientService
    {
        Task<string> SendAsync(string url);
    }
}