using Sewa_Domain.Entities;

namespace Sewa_Application.Contracts.Persistence
{
    public interface IServiceTypeRepository : IAsyncRepository<ServiceType>
    {
        Task<Guid> GetServiceTypeId(Guid serviceTypeId);
    }
}
