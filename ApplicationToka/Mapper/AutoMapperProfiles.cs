using ApplicationToka.Models;
using ApplicationToka.ViewModel;
using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace ApplicationToka.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<PersonDTO, PersonViewModel>().ReverseMap();
            CreateMap<PersonViewModel, PersonDTO>().ReverseMap();
        }
    }
}
