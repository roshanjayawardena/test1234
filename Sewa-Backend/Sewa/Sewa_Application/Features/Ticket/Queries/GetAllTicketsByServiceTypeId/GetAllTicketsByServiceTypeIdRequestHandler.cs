using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sewa_Application.Contracts.Infastructure.Auth;
using Sewa_Application.Exceptions;
using Sewa_Application.Models.Common;

namespace Sewa_Application.Features.Ticket.Queries.GetAllTicketsByServiceTypeId
{ 

    public class GetAllTicketsByServiceTypeIdRequestHandler : IRequestHandler<GetAllTicketsByServiceTypeIdRequest, PaginationResponse<List<TicketDto>>>
    {
       
        private readonly IApplicationDBContext _dbContext;      
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuthService _authService;

        public GetAllTicketsByServiceTypeIdRequestHandler(IApplicationDBContext dbContext, ICurrentUserService currentUserService, IAuthService authService,IMapper mapper)
        {           
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentUserService = currentUserService;
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }
        public async Task<PaginationResponse<List<TicketDto>>> Handle(GetAllTicketsByServiceTypeIdRequest request, CancellationToken cancellationToken)
        {
             var businessUser = await _authService.GetBusinessUserByUserId(_currentUserService.UserId);

             if (businessUser == null) { throw new NotFoundException("Business user not found."); }

            // Get the current date and time
            DateTime currentDate = DateTime.Now;

            // Calculate the start and end of the current day
            DateTime startOfDay = currentDate.Date; // Midnight (00:00:00)
            DateTime endOfDay = startOfDay.AddDays(1).AddTicks(-1); // End of the day (23:59:59.9999999)


            var ordersList = _dbContext.Ticket.Where(p => p.OfficeId == businessUser.OfficeId &&
                                                        p.ServiceTypeId == request.serviceTypeId &&
                                                        p.Status == Sewa_Domain.Common.Enums.ResoloutionStatusEnum.Pending &&
                                                        p.CreatedDate >= startOfDay && p.CreatedDate <= endOfDay)
                                               .Select(w => new TicketDto()
                                               {
                                                    Id = w.Id,                    
                                                    Token = w.Token,                 
                                                    Status = w.Status,
                                                    CreatedDate = w.CreatedDate,

                                               }).AsNoTracking().AsQueryable();

            int totalRecords = await ordersList.CountAsync();
            var itemDetalQuery = ordersList.Skip(request.SkipPageCount).Take(request.PageSize);

            var orderList = _mapper.Map<List<TicketDto>>(itemDetalQuery);
            return new PaginationResponse<List<TicketDto>>(orderList, totalRecords);
        }
    }
}
