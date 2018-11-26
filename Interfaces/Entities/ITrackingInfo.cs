using System;

namespace Interfaces
{
    public interface ITrackingInfo
    {
        Guid Id { get; set; }
        Guid OrderId { get; set; }
        string TrackingNumber { get; set; }
        string PostalCode { get; set; }
        string Address { get; set; }
        string Name { get; set; }
        DateTime CreatedTime { get; set; }
        DateTime EstimatedDeliveryTime  { get; set; }
        TrackingStatus Status { get; set; }
    }
}
