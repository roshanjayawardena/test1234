using MediatR;
using Sewa_Application.Models.Common;

namespace Sewa_Application.Features.Ticket.Queries.GetAllTicketsByServiceTypeId
{
    public class GetAllTicketsByServiceTypeIdRequest: PaginationQuery, IRequest<PaginationResponse<List<TicketDto>>>
    {
        public Guid serviceTypeId { get; set; }
    }
}
