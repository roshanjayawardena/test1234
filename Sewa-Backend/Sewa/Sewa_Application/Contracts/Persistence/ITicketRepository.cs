using Sewa_Domain.Entities;

namespace Sewa_Application.Contracts.Persistence
{
    public interface ITicketRepository: IAsyncRepository<Ticket>
    {
        Task<string> GenerateToken(Guid serviceTypeId);
        Task<List<Ticket>> getAllTicketsbyServiceTypeId(Guid officeId, Guid serviceTypeId);

    }
}
