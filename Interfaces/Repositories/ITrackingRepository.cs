using System;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITrackingRepository
    {
        Task<ITrackingInfo> GetAsync(Guid id);
        Task AddAsync(ITrackingInfo info);
        Task DeleteAsync(Guid id);
    }
}
