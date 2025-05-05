using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Course;

namespace Mini_Moodle.Repositories.CourseService.Commands.CreateCourse
{
    public record CourseCreateCommand(CourseRequestToCreateDto request) : IRequest<OperationResult<CourseResponseCreateUpdateDto>> { }
}
