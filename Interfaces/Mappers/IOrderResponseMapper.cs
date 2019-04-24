using System;

namespace Interfaces
{
    public interface IOrderResponseMapper : IDisposable
    {
        IOrderResponse MapFrom(IOrder order, ITrackingInfo trackingInfo);
    }
}
