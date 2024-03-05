
using AutoMapper;
using DotnetAutoMapEFCoreAPI.Core.DTO;
using DotnetAutoMapEFCoreAPI.Core.Entities;
namespace DotnetAutoMapEFCoreAPI.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            // Tickets
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<Ticket, GetTicketDto>();
            CreateMap<UpdateTicketDto, Ticket>();
        }
    }
}
