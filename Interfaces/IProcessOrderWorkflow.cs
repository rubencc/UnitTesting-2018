using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProcessOrderWorkflow
    {
        Task ProcessOrderAsync(IOrder order);
    }
}
