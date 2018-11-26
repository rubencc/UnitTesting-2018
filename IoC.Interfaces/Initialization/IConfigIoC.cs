using Autofac;

namespace IoC.Interfaces
{
    public interface IConfigIoC
    {
        void BuildIoC(ContainerBuilder container);
    }
}
