using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Sewa_Domain.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Sewa_Application.Contracts.Infastructure.Auth;
using Sewa_Infastructure.Identity;
using Sewa_Domain.Entities;

namespace Sewa_Infastructure.Persistence
{
    public class SewaContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IApplicationDBContext
    {

        private readonly ICurrentUserService _currentUserService;
        public SewaContext(DbContextOptions<SewaContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
           
        }


        public DbSet<Office> Office { get; set; }
         public DbSet<ServiceType> ServiceType { get; set; }
         public DbSet<BusinessUser> BusinessUser { get; set; }
        public DbSet<Ticket> Ticket { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
