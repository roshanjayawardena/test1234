using MediatR;

namespace Sewa_Application.Features.Ticket.Commands.UpdateTicket
{
    public class UpdateTicketCommand: IRequest<Guid>
    {
        public Guid TicketId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
