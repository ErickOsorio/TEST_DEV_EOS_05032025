using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Core.MappingProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}
