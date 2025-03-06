using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace ApplicationToka.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}
