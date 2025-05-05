using AutoMapper;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.AssignmentDto;
using Mini_Moodle.Models.Dto.AssignmentDtos;
using Mini_Moodle.Models.Dto.Course;
using Mini_Moodle.Models.Dto.DifficultyDtos;
using Mini_Moodle.Models.Dto.GradeDto;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Models.Dto.SubmissionDtos;
using Mini_Moodle.Repositories.CourseService.Commands.CreateCourse;

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
        CreateMap<Lesson, LessonResponseDto>().ReverseMap();

        CreateMap<Assignment,AssignmentRequestDto>().ReverseMap();
        CreateMap<Assignment,AssignmentResponseDto>().ReverseMap();
        CreateMap<Assignment, AssignmentResponseForUserDto>().ReverseMap();   
        CreateMap<Assignment, AssignmentRequestToUpdateDto>().ReverseMap();   

        CreateMap<Submission, SubmissionResponseDto>().ReverseMap();
        CreateMap<Submission, SubmissionResponseForUserDto>().ReverseMap();
        CreateMap<Submission, SubmissionRequestDto>().ReverseMap();
        CreateMap<Submission, SubmissionRequestToUpdate>().ReverseMap();

        CreateMap<Grade,GradeRequestDto>().ReverseMap();  
        CreateMap<Grade,GradeResponseDto>().ReverseMap();

        CreateMap<Difficulty, DifficultyResponseDto>().ReverseMap();
    }
}
