using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sewa_Application.Contracts.Infastructure.Auth;
using Sewa_Application.Exceptions;
using Sewa_Application.Features.Auth.Commands.Login;
using Sewa_Application.Models.Auth;
using Sewa_Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sewa_Infastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IApplicationDBContext _applicationDBContext;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config, IApplicationDBContext applicationDBContext)
        {
            _userManager = userManager;
            _config = config;
            _applicationDBContext = applicationDBContext;
        }


        private async Task<string> GenerateTokenString(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.BusinessUserId.ToString()),             
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Secret").Value));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(300),
                issuer: _config.GetSection("Jwt:ValidIssuer").Value,
                audience: _config.GetSection("Jwt:ValidAudience").Value,
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<LoginDto> Login(LoginUser user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            if (identityUser != null)
            {
                var result = await _userManager.CheckPasswordAsync(identityUser, user.Password);
                if (result)
                {
                    var accessToken = await GenerateTokenString(identityUser);                
                    return new LoginDto
                    {
                        AccessToken = accessToken,
                     
                    };

                }
                else
                    throw new UnAuthorizedException("The password is incorrect");
            }
            throw new UnAuthorizedException("The Email is incorrect,Please contact administrator.");
        }

        public async Task<BusinessUser> GetBusinessUserByUserId(Guid userId)
        {
            var businessUser = await _applicationDBContext.BusinessUser.FirstOrDefaultAsync(w => w.Id == userId);
            if (businessUser != null) { 
            
               return businessUser;
            }
            return null;
        }
    }
}
