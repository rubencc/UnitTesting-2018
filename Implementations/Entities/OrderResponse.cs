using System;
using Interfaces;

namespace Implementations
{
    public class OrderResponse : IOrderResponse
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public ITrackingInfo TrackingInfo { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int NumberOfItems { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
    }
}
