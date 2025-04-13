using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto;

namespace Mini_Moodle.Repositories.Courses.Commands
{
    public record CourseCreateCommand(CourseRequestToCreateDto request) : IRequest<OperationResult<CourseResponseUpdateDto>> { }
}
