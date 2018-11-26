using System;

namespace Interfaces
{
    public interface ITrackingService
    {
        ITrackingInfo CreateTrackingInfo(Guid orderId, string postalCode, string address);
    }
}
