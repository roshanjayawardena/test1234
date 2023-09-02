using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sewa_Application.Features.Ticket.Commands.ChangeTicketStatus;
using Sewa_Application.Features.Ticket.Commands.CreateToken;
using Sewa_Application.Features.Ticket.Commands.UpdateTicket;
using Sewa_Application.Features.Ticket.Queries.GetAllTicketsByServiceTypeAndStatus;
using Sewa_Application.Features.Ticket.Queries.GetAllTicketsByServiceTypeId;
using Sewa_Application.Models.Common;
using System.Net;

namespace Sewa.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost(Name = "CreateToken")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<TokenDto>> CreateToken([FromBody] CreateTokenUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }      

        [HttpGet("{serviceTypeId}", Name = "GetAllTickets")]        
        public async Task<ActionResult<PaginationResponse<List<TicketDto>>>> GetAllTickets([FromQuery] GetAllTicketsByServiceTypeIdRequest searchedOrderQuery, string serviceTypeId)
        {            
            var result = await _mediator.Send(new GetAllTicketsByServiceTypeIdRequest() { serviceTypeId = Guid.Parse(serviceTypeId),PageIndex = searchedOrderQuery.PageIndex , PageSize = searchedOrderQuery.PageSize});
            return Ok(result);
        }

        [HttpGet("{serviceTypeId}/{status}", Name = "GetAllTicketsByServiceTypeAndStatus")]
        public async Task<ActionResult<PaginationResponse<List<TicketDto>>>> GetAllTicketsByServiceTypeAndStatus([FromQuery] GetAllTicketsByServiceTypeAndStatusRequest searchedOrderQuery, string serviceTypeId, int status)
        {            
            var result = await _mediator.Send(new GetAllTicketsByServiceTypeAndStatusRequest() { serviceTypeId = Guid.Parse(serviceTypeId), status = status, PageIndex = searchedOrderQuery.PageIndex, PageSize = searchedOrderQuery.PageSize });
            return Ok(result);
        }


        [HttpPut("{id}", Name = "ChangeStatus")]
        public async Task<ActionResult<bool>> ChangeStatus([FromBody] ChangeTicketStatusCommand command, Guid id)
        {           
            var result = await _mediator.Send(command);
            return Ok(result);
        }

     
        [HttpPut(Name = "UpdateTicket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> UpdateTicket([FromBody] UpdateTicketCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
