using Sewa_Domain.Common.Enums;

namespace Sewa_Application.Features.Ticket.Queries.GetAllTicketsByServiceTypeId
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public ResoloutionStatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
