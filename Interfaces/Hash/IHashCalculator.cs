using System;

namespace Interfaces
{
    public interface IHashCalculator : IDisposable
    {
        string Hash(string input);
    }
}
