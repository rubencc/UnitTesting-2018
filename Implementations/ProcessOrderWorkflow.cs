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

        public ProcessOrderWorkflow(ITracer tracer, ITrackingService trackingService, IProxyRepository proxyRepository)
        {
            this.tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            this.trackingService = trackingService ?? throw new ArgumentNullException(nameof(trackingService));
            this.proxyRepository = proxyRepository ?? throw new ArgumentNullException(nameof(proxyRepository));
        }

        public Task ProcessOrderAsync(IOrder order)
        {
            return Task.CompletedTask;
        }
    }
}
