using System;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProxyRepository : IDisposable
    {
        Task<IOrderResponse> AddResponseAsync(IOrderResponse response);
    }
}
