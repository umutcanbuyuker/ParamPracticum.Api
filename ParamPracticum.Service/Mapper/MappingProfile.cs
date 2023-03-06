using AutoMapper;
using ParamPracticum.Data.Models;
using ParamPracticum.Dto.Dtos;

namespace ParamPracticum.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>(); 
        }
    }
}
