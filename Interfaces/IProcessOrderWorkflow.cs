using System;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProcessOrderWorkflow : IDisposable
    {
        Task ProcessOrderAsync(IOrder order);
    }
}
