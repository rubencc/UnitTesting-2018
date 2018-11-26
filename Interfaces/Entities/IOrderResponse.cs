using System;

namespace Interfaces
{
    public interface IOrderResponse
    {
        Guid Id { get; set; }
        Guid OrderId { get; set; }
        ITrackingInfo TrackingInfo { get; set; }
    }
}
