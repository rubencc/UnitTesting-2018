using System;

namespace Interfaces
{
    public interface ITracer : IDisposable
    {
        void Log(string message, int category);
    }
}
