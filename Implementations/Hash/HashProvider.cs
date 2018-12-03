using System;
using System.Security.Cryptography;
using Interfaces;

namespace Implementations
{
    public class HashProvider : IHashProvider
    {
        private readonly MD5CryptoServiceProvider provider;

        public HashProvider()
        {
            this.provider = new MD5CryptoServiceProvider();
        }

        public byte[] ComputeHash(byte[] buffer)
        {
            byte[] bytes = provider.ComputeHash(buffer);

            return bytes;

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.provider?.Dispose();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
