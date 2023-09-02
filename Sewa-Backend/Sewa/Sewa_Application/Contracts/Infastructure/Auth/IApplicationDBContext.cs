using Microsoft.EntityFrameworkCore;
using Sewa_Domain.Entities;

namespace Sewa_Application.Contracts.Infastructure.Auth
{
    public interface IApplicationDBContext
    {
         DbSet<Office> Office { get; set; }
         DbSet<ServiceType> ServiceType { get; set; }
         DbSet<BusinessUser> BusinessUser { get; set; }
         DbSet<Ticket> Ticket { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
