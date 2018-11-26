using System;

namespace IoC.Interfaces
{
    public interface IFactory: IDisposable
    {
        T Resolve<T>();
    }
}
