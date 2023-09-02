using AutoMapper;
using Sewa_Application.Features.Auth.Commands.Login;
using Sewa_Application.Features.Category.Queries.GetAllCategories;
using Sewa_Application.Features.Ticket.Queries.GetAllTicketsByServiceTypeId;
using Sewa_Application.Models.Auth;
using Sewa_Domain.Entities;

namespace Sewa_Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<LoginUserCommand, LoginUser>().ForMember(i => i.UserName, opts => opts.MapFrom(i => i.Email));
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<ServiceType, CategoryDto>().ReverseMap();
        }
    }
}
