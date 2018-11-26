using Autofac;
using IoC.Interfaces;

namespace IoC.Implementation
{
    public class Initialization : IConfigIoC
    {
        public void BuildIoC(ContainerBuilder container)
        {
            container.RegisterType<DependencyResolver>();
            container.RegisterType<Factory>().As<IFactory>();
            
        }
    }
}
