using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sewa_Application.Contracts.Infastructure.Auth;
using Sewa_Application.Contracts.Persistence;
using Sewa_Application.Exceptions;

namespace Sewa_Application.Features.Ticket.Commands.CreateToken
{
    public class CreateTokenUserCommandHandler : IRequestHandler<CreateTokenUserCommand, TokenDto>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;    
       
        private readonly ILogger<CreateTokenUserCommand> _logger;

        public CreateTokenUserCommandHandler(ITicketRepository ticketRepository,IServiceTypeRepository serviceTypeRepository, IAuthService authService, ICurrentUserService currentUserService,IMapper mapper, ILogger<CreateTokenUserCommand> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _serviceTypeRepository = serviceTypeRepository ?? throw new ArgumentNullException(nameof(serviceTypeRepository));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _currentUserService = currentUserService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<TokenDto> Handle(CreateTokenUserCommand request, CancellationToken cancellationToken)
        {
            var ticketToken =  await _ticketRepository.GenerateToken(request.serviceTypeId);

            var serviceTypeId= await _serviceTypeRepository.GetServiceTypeId(request.serviceTypeId);

            if (serviceTypeId == Guid.Empty) {

                throw new NotFoundException("Service type Id not found");
            }

            var businessUser = await _authService.GetBusinessUserByUserId(_currentUserService.UserId);

            if (businessUser == null)
            {
                throw new NotFoundException("Busines user not found");
            }

            var ticketEntity = new Sewa_Domain.Entities.Ticket()
            {
                Token = ticketToken,
                OfficeId = businessUser.OfficeId,
                ServiceTypeId = serviceTypeId,
                Status = Sewa_Domain.Common.Enums.ResoloutionStatusEnum.Pending
            };

            var newProperty = await _ticketRepository.AddAsync(ticketEntity);
            _logger.LogInformation($"Ticket {newProperty.Id} is successfully created.");
            return new TokenDto() { Token = ticketToken };
          
        }
    }
}
