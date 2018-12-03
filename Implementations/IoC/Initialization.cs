using Autofac;
using Interfaces;
using IoC.Interfaces;

namespace Implementations
{
    public class Initialization : IConfigIoC
    {
        public void BuildIoC(ContainerBuilder container)
        {
            container.RegisterType<ProcessOrderWorkflow>().As<IProcessOrderWorkflow>();
            container.RegisterType<OrderResponse>().As<IOrderResponse>();
            container.RegisterType<Order>().As<IOrder>();
            container.RegisterType<TrackingInfo>().As<ITrackingInfo>();
            

            container.RegisterType<HashProvider>().As<IHashProvider>();
            container.RegisterType<HashCalculator>().As<IHashCalculator>();
        }
    }
}
