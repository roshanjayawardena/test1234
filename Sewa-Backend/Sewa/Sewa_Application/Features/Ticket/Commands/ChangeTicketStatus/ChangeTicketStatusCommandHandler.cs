using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sewa_Application.Contracts.Infastructure.Auth;
using Sewa_Application.Exceptions;
using Sewa_Domain.Common.Enums;

namespace Sewa_Application.Features.Ticket.Commands.ChangeTicketStatus
{

    public class ChangeTicketStatusCommandHandler : IRequestHandler<ChangeTicketStatusCommand, bool>
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ChangeTicketStatusCommand> _logger;

        public ChangeTicketStatusCommandHandler(IApplicationDBContext dBContext, IMapper mapper, ILogger<ChangeTicketStatusCommand> logger)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> Handle(ChangeTicketStatusCommand request, CancellationToken cancellationToken)
        {
            var ticketEntity = await _dbContext.Ticket.FirstOrDefaultAsync(w => w.Id == request.TicketId);
            if (ticketEntity == null)
            {
                throw new NotFoundException(nameof(Sewa_Domain.Entities.Ticket), request.TicketId);
            }
            if (ticketEntity != null)
            {
                ticketEntity.Status = (ResoloutionStatusEnum)request.Status;
                ticketEntity.ServiceStartDateTime= DateTime.UtcNow;
                await _dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Ticket status has successfully updated.");
                return true;
            }
            return false;
        }
    }
}
