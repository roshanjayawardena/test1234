using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sewa_Application.Contracts.Infastructure.Auth;
using Sewa_Application.Contracts.Persistence;
using Sewa_Infastructure.Identity;
using Sewa_Infastructure.Persistence;
using Sewa_Infastructure.Repositories;
using Sewa_Infastructure.Seed;
using System.Text;

namespace Sewa_Infastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static async Task<IServiceCollection> ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add dbcontext
            services.AddDbContext<SewaContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("SewaConnectionString")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                           .GetBytes(configuration.GetSection("JWT:Secret").Value)),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero
                   };
               });


            // Add Identity
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<SewaContext>()
                .AddDefaultTokenProviders();


            // Config Identity
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 7;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
            });

            // Register the RoleSeeder
            services.AddScoped<SewaContextSeed>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            // Seed roles
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var roleSeeder = scope.ServiceProvider.GetRequiredService<SewaContextSeed>();
                await roleSeeder.SeedRolesAsync();
                await roleSeeder.SeedAdminUserAsync();
            }

            services.AddScoped<IApplicationDBContext, SewaContext>();
            services.AddTransient(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IServiceTypeRepository, ServiceTypeRepository>();       
           
            return services;
        }
    }
}
