using System;
using Interfaces;

namespace Implementations
{
    public class TrackingInfo : ITrackingInfo
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public TrackingStatus Status { get; set; }
    }
}
