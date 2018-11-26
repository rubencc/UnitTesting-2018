using Autofac;
using Interfaces;
using IoC.Implementation;
using IoC.Interfaces;
using UglyLoader;

namespace ConsoleTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            IDependencyResolver resolver = GetResolver();

            using (var scope = resolver.BeginScope())
            {
                IFactory factory = (IFactory)scope.GetService(typeof(IFactory));

                IProcessOrderWorkflow workflow = factory.Resolve<IProcessOrderWorkflow>();
            }

        }

        private static IDependencyResolver GetResolver()
        {
            var container = new ContainerBuilder();    

            UglyLoaderApiBuilder.Build()
                .LoadAssemblies<IConfigIoC>(act => act.BuildIoC(container));

            var builder = container.Build();

            IDependencyResolver resolver = builder.Resolve<DependencyResolver>();

            return resolver;
        }
    }
}
