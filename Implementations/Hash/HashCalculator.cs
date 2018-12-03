using System;
using System.Text;
using System.Text.RegularExpressions;
using Interfaces;

namespace Implementations
{
    public class HashCalculator : IHashCalculator
    {
        private readonly IHashProvider provider;

        public HashCalculator(IHashProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public string Hash(string input)
        {
            StringBuilder hash = new StringBuilder();

            byte[] bytes = this.provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            foreach (var item in bytes)
            {
                hash.Append(item.ToString("x2"));
            }

            return Regex.Replace(hash.ToString(), "[^0-9.]", "");

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

        ~HashCalculator()
        {
            this.Dispose(false);
        }
    }
}
