using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sewa_Application.Contracts.Persistence;
using Sewa_Application.Exceptions;
using Sewa_Domain.Common.Enums;

namespace Sewa_Application.Features.Ticket.Commands.UpdateTicket
{

    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, Guid>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;      
        private readonly ILogger<UpdateTicketCommand> _logger;

        public UpdateTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper, ILogger<UpdateTicketCommand> logger)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));          
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Guid> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {

            var ticketEntity = await _ticketRepository.GetByIdAsync(request.TicketId);

            if (ticketEntity != null)
            {

                ticketEntity.Description = request.Description;
                ticketEntity.ServiceEndDateTime = DateTime.UtcNow;
                ticketEntity.Status = (ResoloutionStatusEnum)request.Status;

                await _ticketRepository.UpdateAsync(ticketEntity);
                return request.TicketId;
            }
            throw new NotFoundException(nameof(Sewa_Domain.Entities.Ticket), request.TicketId);
        }
    }
}
