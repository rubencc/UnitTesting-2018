using System;
using Interfaces;
using IoC.Interfaces;

namespace Implementations
{
    public class OrderResponseMapper : IOrderResponseMapper
    {
        private readonly IFactory factory;

        public OrderResponseMapper(IFactory factory)
        {
            this.factory = factory;
        }

        public IOrderResponse MapFrom(IOrder order, ITrackingInfo trackingInfo)
        {
            IOrderResponse response = this.factory.Resolve<IOrderResponse>();

            response.Id = Guid.NewGuid();
            response.OrderId = order.Id;
            response.TrackingInfo = trackingInfo;
            response.Address = order.Address;
            response.PostalCode = order.PostalCode;
            response.Name = order.Name;
            response.NumberOfItems = order.NumberOfItems;
            response.Sku = order.Sku;
            return response;
        }

        private void Dispose(bool disposing)
        {

            if (disposing)
            {
                this.factory?.Dispose();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~OrderResponseMapper()
        {
            this.Dispose(false);
        }
    }
}
