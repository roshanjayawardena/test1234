using Sewa_Application.Features.Auth.Commands.Login;
using Sewa_Application.Models.Auth;
using Sewa_Domain.Entities;

namespace Sewa_Application.Contracts.Infastructure.Auth
{
    public interface IAuthService
    {
        Task<LoginDto> Login(LoginUser user);
        Task<BusinessUser> GetBusinessUserByUserId(Guid userId);
    }
}
