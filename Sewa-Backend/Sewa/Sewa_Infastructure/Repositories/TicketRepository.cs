using Microsoft.EntityFrameworkCore;
using Sewa_Application.Contracts.Persistence;
using Sewa_Application.Exceptions;
using Sewa_Domain.Entities;
using Sewa_Infastructure.Persistence;

namespace Sewa_Infastructure.Repositories
{
    public class TicketRepository: RepositoryBase<Ticket>, ITicketRepository
    {
        
        public TicketRepository(SewaContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<string> GenerateToken(Guid serviceTypeId)
        {
            var serviceType = await _dbContext.ServiceType.FirstOrDefaultAsync(w => w.Id == serviceTypeId);
            if (serviceType == null)
            {
                throw new NotFoundException("Service Type not found");
            }
            
            DateTime currentDate = DateTime.Now;
           
            DateTime startOfDay = currentDate.Date;
            DateTime endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            // Count records created within the current day
            var maxRecordCount = _dbContext.Ticket.Count(w => w.CreatedDate >= startOfDay && w.CreatedDate <= endOfDay);
          
            var incrementedNo = ++ maxRecordCount;
            string paddedNumber = incrementedNo.ToString("D3"); // 3-digit padded number
            string token = $"{paddedNumber}-{serviceType.Code}";
            return token;
        }

        public async Task<List<Ticket>> getAllTicketsbyServiceTypeId(Guid officeId, Guid serviceTypeId) {
           
            DateTime currentDate = DateTime.Now;
           
            DateTime startOfDay = currentDate.Date; 
            DateTime endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            var ticketList = await _dbContext.Ticket.Where(p => p.OfficeId == officeId && 
                                                        p.ServiceTypeId == serviceTypeId && 
                                                        p.Status == Sewa_Domain.Common.Enums.ResoloutionStatusEnum.Pending &&  
                                                        p.CreatedDate >= startOfDay && p.CreatedDate <= endOfDay).ToListAsync();
            return ticketList;
        }
    }
}
