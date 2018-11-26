using System;
using Interfaces;

namespace Implementations
{
    public class Order : IOrder
    {
        public Guid Id { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int NumberOfItems { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
    }
}
