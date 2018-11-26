using System;
using IoC.Interfaces;

namespace IoC.Implementation
{
    public class Factory : IFactory
    {
        private DependencyResolver resolver;
        public bool IsDisposed { get; protected set; }

        public Factory(DependencyResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            this.resolver = resolver;
        }

        public T Resolve<T>()
        {
            using (var scope = resolver.BeginScope())
            {
                return (T)scope.GetService(typeof(T));
            }
        }

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

            this.resolver?.Dispose();
            this.resolver = null;
            this.IsDisposed = true;
        }
    }
}
