using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProxyRepository
    {
        Task<IOrderResponse> AddResponseAsync(IOrderResponse response);
    }
}
