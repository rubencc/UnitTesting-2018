using System;

namespace Interfaces
{
    public interface IOrderResponse
    {
        Guid Id { get; set; }
        Guid OrderId { get; set; }
        ITrackingInfo TrackingInfo { get; set; }
        string PostalCode { get; set; }
        string Address { get; set; }
        int NumberOfItems { get; set; }
        string Sku { get; set; }
        string Name { get; set; }
    }
}
