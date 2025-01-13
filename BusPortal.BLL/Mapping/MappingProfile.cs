using AutoMapper;

namespace BusPortal.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map between BLL Client and DAL Client
            CreateMap<BusPortal.BLL.Domain.Models.Client, BusPortal.DAL.Persistence.Entities.Client>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Admin, opt => opt.MapFrom(src => src.Admin));
        }
    }
}
