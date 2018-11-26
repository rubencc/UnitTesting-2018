using Autofac;
using IoC.Interfaces;

namespace IoC.Implementation
{
    public class DependencyResolver : ScopeContainer, IDependencyResolver
    {
        public DependencyResolver(ILifetimeScope container) : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            var child =  this.Container.BeginLifetimeScope();
            return new ScopeContainer(child);
        }
    }
}
