using System;
using System.Threading.Tasks;
using Interfaces;

namespace Implementations
{
    public class ProcessOrderWorkflow : IProcessOrderWorkflow
    {
        private readonly ITracer tracer;
        private readonly ITrackingService trackingService;
        private readonly IProxyRepository proxyRepository;
        private readonly IOrderResponseMapper mapper;

        public ProcessOrderWorkflow(ITracer tracer, ITrackingService trackingService, IProxyRepository proxyRepository, IOrderResponseMapper mapper)
        {
            this.tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            this.trackingService = trackingService ?? throw new ArgumentNullException(nameof(trackingService));
            this.proxyRepository = proxyRepository ?? throw new ArgumentNullException(nameof(proxyRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task ProcessOrderAsync(IOrder order)
        {
            try
            {
                ITrackingInfo trackingInfo = await this.trackingService.CreateTrackingInfoAsync(order.Id, order.PostalCode, order.Address, order.Name)
                    .ConfigureAwait(false);

                IOrderResponse response = this.mapper.MapFrom(order, trackingInfo);

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



        private void Dispose(bool disposing)
        {

            if (disposing)
            {
                this.mapper?.Dispose();
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
