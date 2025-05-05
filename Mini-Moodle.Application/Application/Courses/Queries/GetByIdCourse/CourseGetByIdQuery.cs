using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Course;

namespace Mini_Moodle.Repositories.CourseService.Queries.GetByIdCourse
{
    public record CourseGetByIdQuery(Guid id) : IRequest<OperationResult<CourseResponseDto>> { }
}
