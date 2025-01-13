using AutoMapper;
using BusPortal.Common.Models;
using BusPortal.Web.Models.DTO;

namespace BusPortal.Web.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create the mapping between ViewModel and DTO
            CreateMap<BusPortal.Common.Models.RegisterViewModel, BusPortal.Web.Models.DTO.RegisterViewModel>();
            CreateMap<BusPortal.Web.Models.DTO.RegisterViewModel, BusPortal.Common.Models.RegisterViewModel>();
        }
    }
}
