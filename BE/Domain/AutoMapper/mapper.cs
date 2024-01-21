
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Domain.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            // Additional mappings...
        }
    }
    
}


