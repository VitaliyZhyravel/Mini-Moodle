using AutoMapper;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto;

namespace Mini_Moodle.Automapper
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
           CreateMap<Course,CourseResponseDto>().ReverseMap();
            CreateMap<Course,CourseRequestToCreateDto>().ReverseMap();
            CreateMap<CourseResponseUpdateDto,Course>().ReverseMap();
        }
    }
}
