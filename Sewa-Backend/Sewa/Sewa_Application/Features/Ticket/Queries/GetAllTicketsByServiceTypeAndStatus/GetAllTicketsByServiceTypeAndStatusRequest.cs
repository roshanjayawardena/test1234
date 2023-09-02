using MediatR;
using Sewa_Application.Features.Ticket.Queries.GetAllTicketsByServiceTypeId;
using Sewa_Application.Models.Common;

namespace Sewa_Application.Features.Ticket.Queries.GetAllTicketsByServiceTypeAndStatus
{
    public class GetAllTicketsByServiceTypeAndStatusRequest : PaginationQuery, IRequest<PaginationResponse<List<TicketDto>>>
    {
        public Guid serviceTypeId { get; set; }
        public int status { get; set; }
    }
    
}
