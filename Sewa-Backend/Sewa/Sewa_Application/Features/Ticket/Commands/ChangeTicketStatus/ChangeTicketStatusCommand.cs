using MediatR;

namespace Sewa_Application.Features.Ticket.Commands.ChangeTicketStatus
{
    public class ChangeTicketStatusCommand: IRequest<bool>
    {
        public Guid TicketId { get; set; }
        public int Status { get; set; }
    }
}
