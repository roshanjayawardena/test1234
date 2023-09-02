using Microsoft.AspNetCore.Identity;

namespace Sewa_Infastructure.Identity
{
    public class ApplicationRole: IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
