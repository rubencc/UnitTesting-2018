using System;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITrackingService : IDisposable
    {
        Task<ITrackingInfo> CreateTrackingInfoAsync(Guid orderId, string postalCode, string address, string name);
        Task CancelTrackingAsync(Guid orderId);
    }
}
