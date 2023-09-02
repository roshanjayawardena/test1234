using MediatR;

namespace Sewa_Application.Features.Ticket.Commands.CreateToken
{
    public class CreateTokenUserCommand : IRequest<TokenDto>
    {
        public Guid serviceTypeId { get; set; }

    }
}
