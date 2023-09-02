using Microsoft.EntityFrameworkCore;
using Sewa_Application.Contracts.Persistence;
using Sewa_Domain.Entities;
using Sewa_Infastructure.Persistence;

namespace Sewa_Infastructure.Repositories
{
    public class ServiceTypeRepository : RepositoryBase<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(SewaContext dbContext) : base(dbContext)
        {

        }

        public async Task<Guid> GetServiceTypeId(Guid serviceTypeId)
        {
            var serviceTypeDomain = await _dbContext.ServiceType.FirstOrDefaultAsync(w => w.Id == serviceTypeId);

            if(serviceTypeDomain != null) {
               return serviceTypeDomain.Id;
            }

            return Guid.Empty;
        }
    }

}
