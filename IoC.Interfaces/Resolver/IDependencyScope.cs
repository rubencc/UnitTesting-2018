using System;
using System.Collections.Generic;

namespace IoC.Interfaces
{
    public interface IDependencyScope : IDisposable
    {
        object GetService(Type serviceType);

        IEnumerable<object> GetServices(Type serviceType);
    }
}
