using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto;

namespace Mini_Moodle.Repositories.Courses.Queries
{
    public record CourseGetByIdQuery(Guid id) : IRequest<OperationResult<CourseResponseDto>> { }
}
