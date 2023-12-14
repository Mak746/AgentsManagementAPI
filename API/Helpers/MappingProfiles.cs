using API.Dtos;
using AutoMapper;
using Core.Entities;

using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, Core.Entities.Identity.Address>();
            CreateMap<Core.Entities.Agent, AgentsDto>().ReverseMap();
            CreateMap<AgentsDto, Core.Entities.Agent>();
            CreateMap<Core.Entities.Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryDto, Core.Entities.Category>();

        }
    }
}