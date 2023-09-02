using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sewa_Application.Contracts.Infastructure.Auth;
using Sewa_Application.Models.Auth;

namespace Sewa_Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginDto>
    {

        private readonly IMapper _mapper;      
        private readonly IAuthService _authService;
        private readonly ILogger<LoginUserCommand> _logger;

        public LoginUserCommandHandler(IAuthService authService, IMapper mapper, ILogger<LoginUserCommand> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));           
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loginModel = _mapper.Map<LoginUser>(request);
            return await _authService.Login(loginModel);
        }
    }
}
