using System;
using System.Collections.Generic;
using Autofac;
using IoC.Interfaces;

namespace IoC.Implementation
{
    public class ScopeContainer: IDependencyScope
    {
        protected ILifetimeScope Container;
        public bool IsDisposed { get; protected set; }


        public ScopeContainer(ILifetimeScope container)
        {
            this.Container = container ?? throw new ArgumentNullException(nameof(container));
        }

        ~ScopeContainer()
        {
            this.Dispose(false);
        }


        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.IsDisposed)
            {
                return;
            }

            this.Container?.Dispose();
            this.Container = null;
            this.IsDisposed = true;
        }

        public object GetService(Type serviceType)
        {
            return this.Container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.ResolveAll(serviceType);
        }

        private IEnumerable<T> ResolveAll<T>(T serviceType) where T : class
        {
            return this.Container.Resolve<IEnumerable<T>>();
        }
    }
}
