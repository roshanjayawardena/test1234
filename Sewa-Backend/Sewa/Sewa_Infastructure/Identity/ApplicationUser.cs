using Microsoft.AspNetCore.Identity;
using Sewa_Domain.Entities;

namespace Sewa_Infastructure.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public Guid BusinessUserId { get; set; }
        public BusinessUser BusinessUser { get; set; }
    }
}
