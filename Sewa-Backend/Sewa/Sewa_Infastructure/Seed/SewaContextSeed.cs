using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Sewa_Application.Helpers;
using Sewa_Domain.Common.Enums;
using Sewa_Domain.Entities;
using Sewa_Infastructure.Identity;

namespace Sewa_Infastructure.Seed
{
    public class SewaContextSeed
    {

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public SewaContextSeed(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task SeedRolesAsync()
        {

            var roles = new List<ApplicationRole>
                {
                   new ApplicationRole("Reception"),
                   new ApplicationRole("Service Provider"),                  
                   new ApplicationRole("Admin"),
                };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role.Name))
                    await _roleManager.CreateAsync(role);
            };



            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new ApplicationRole
                {
                    Name = "Admin"
                };

                await _roleManager.CreateAsync(adminRole);
            }
        }

        public async Task SeedAdminUserAsync()
        {
            var adminEmail = _configuration.GetSection("AdminUserEmail").Value;
            var adminPassword = _configuration.GetSection("AdminUserPassword").Value;

            //Super Admin User
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var office = new Office()
                {
                    Address = "No 12/A Park Street, Colombo 10",
                    Region="Colombo",
                    State="Western"
                };
                var businessUser = new BusinessUser()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    Status = BusinessUserStatusEnum.Active,
                    Office = office,
                };
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    BusinessUser = businessUser
                };
                await _userManager.CreateAsync(adminUser, adminPassword);
                await _userManager.AddToRoleAsync(adminUser, RoleHelper.Admin);
            }

            // add receiption user

            var receiptionEmail = "Paboda@gmail.com";
            var receiptionPassword = "Abc@1234";

            var receiptionUser = await _userManager.FindByEmailAsync(receiptionEmail);
            if (receiptionUser == null)
            {
                //var office = new Office()
                //{
                //    Address = "No 12/A Temple Street, Peradeniya",
                //    Region = "Kandy",
                //    State = "Western"
                //};
                var businessUser = new BusinessUser()
                {
                    Id = Guid.NewGuid(),
                    Name = "Receiptant",
                    Status = BusinessUserStatusEnum.Active,
                    OfficeId = Guid.Parse("31AAEE93-C800-4A08-9E09-08DBAA3E727C"),
                };
                receiptionUser = new ApplicationUser
                {
                    UserName = receiptionEmail,
                    Email = receiptionEmail,
                    BusinessUser = businessUser
                };
                await _userManager.CreateAsync(receiptionUser, receiptionPassword);
                await _userManager.AddToRoleAsync(receiptionUser, RoleHelper.Reception);
            }

            // add service provider user

            var serviceProviderEmail = "Eashan@gmail.com";
            var serviceProviderPassword = "Abc@1234";

            var serviceProviderUser = await _userManager.FindByEmailAsync(serviceProviderEmail);
            if (serviceProviderUser == null)
            {
                //var office = new Office()
                //{
                //    Address = "No 12/A Temple Street, Peradeniya",
                //    Region = "Kandy",
                //    State = "Western"
                //};
                var businessUser = new BusinessUser()
                {
                    Id = Guid.NewGuid(),
                    Name = "ServiceProvider",
                    Status = BusinessUserStatusEnum.Active,
                    OfficeId = Guid.Parse("31AAEE93-C800-4A08-9E09-08DBAA3E727C"),
                };
                serviceProviderUser = new ApplicationUser
                {
                    UserName = serviceProviderEmail,
                    Email = serviceProviderEmail,
                    BusinessUser = businessUser
                };
                await _userManager.CreateAsync(serviceProviderUser, serviceProviderPassword);
                await _userManager.AddToRoleAsync(serviceProviderUser, RoleHelper.ServiceProvider);
            }

        }

        public static async Task SeedUserRoleAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration config)
        {
            var roles = new List<ApplicationRole>
                {
                   new ApplicationRole("Reception"),
                   new ApplicationRole("Service Provider"),                   
                   new ApplicationRole("Admin"),
                };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                    await roleManager.CreateAsync(role);
            };

            var adminEmail = config.GetSection("AdminUserEmail").Value;
            var adminPassword = config.GetSection("AdminUserPassword").Value;

            //Admin User
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };
                await userManager.CreateAsync(adminUser, adminPassword);
                await userManager.AddToRoleAsync(adminUser, RoleHelper.Admin);
            }
        }
    }
}
