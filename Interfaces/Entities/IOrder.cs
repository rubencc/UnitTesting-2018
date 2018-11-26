using System;

namespace Interfaces
{
    public interface IOrder
    {
        Guid Id { get; set; }
        string PostalCode { get; set; }
        string Address { get; set; }
        int NumberOfItems { get; set; }
        string Sku { get; set; }
        string Name { get; set; }
    }
}
