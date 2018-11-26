using System;
using Interfaces;

namespace Implementations
{
    public class OrderResponse : IOrderResponse
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public ITrackingInfo TrackingInfo { get; set; }
    }
}
