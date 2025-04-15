using AutoMapper;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Course;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Automapper;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<Course, CourseResponseDto>().ReverseMap();
        CreateMap<Course, CourseRequestToCreateDto>().ReverseMap();
        CreateMap<CourseResponseCreateUpdateDto, Course>().ReverseMap();
        CreateMap<Course, CourseRequestToUpdateDto>().ReverseMap();
        CreateMap<LessonRequestToCreateDto, Lesson>().ReverseMap();
        CreateMap<Lesson, LessonResponseToCreateUpdateDto>().ReverseMap();

    }
}
