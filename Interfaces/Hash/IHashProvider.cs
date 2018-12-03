using System;

namespace Interfaces
{
    public interface IHashProvider : IDisposable
    {
        byte[] ComputeHash(byte[] buffer);
    }
}
