using System;
using System.Threading.Tasks;
using Interfaces;
using IoC.Interfaces;

namespace Implementations
{
    public class ProcessOrderWorkflow : IProcessOrderWorkflow
    {
        private readonly ITracer tracer;
        private readonly ITrackingService trackingService;
        private readonly IProxyRepository proxyRepository;
        private readonly IFactory factory;

        public ProcessOrderWorkflow(ITracer tracer, ITrackingService trackingService, IProxyRepository proxyRepository, IFactory factory)
        {
            this.tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            this.trackingService = trackingService ?? throw new ArgumentNullException(nameof(trackingService));
            this.proxyRepository = proxyRepository ?? throw new ArgumentNullException(nameof(proxyRepository));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task ProcessOrderAsync(IOrder order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            try
            {
                ITrackingInfo trackingInfo = await this.trackingService.CreateTrackingInfoAsync(order.Id, order.PostalCode, order.Address, order.Name)
                    .ConfigureAwait(false);

                IOrderResponse response = this.GetOrderResponse(order, trackingInfo);

                await this.proxyRepository.AddResponseAsync(response).ConfigureAwait(false);

                this.tracer.Log($"Order {order.Id} processed", (int)CategoryLog.Info);
            }
            catch (Exception ex)
            {
                this.tracer.Log($"Order {order.Id} not processed", (int)CategoryLog.Info);
                this.tracer.Log(ex.Message, (int)CategoryLog.Error);

                await this.trackingService.CancelTrackingAsync(order.Id).ConfigureAwait(false);
                this.tracer.Log($"Tracking {order.Id} canceled", (int)CategoryLog.Info);                 
            }
        }

        private IOrderResponse GetOrderResponse(IOrder order, ITrackingInfo trackingInfo)
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
                this.trackingService?.Dispose();
                this.proxyRepository?.Dispose();
                this.tracer?.Dispose();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ProcessOrderWorkflow()
        {
            this.Dispose(false);
        }
    }
}
