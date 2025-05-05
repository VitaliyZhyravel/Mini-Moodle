using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Course;

namespace Mini_Moodle.Repositories.CourseService.Commands.DeleteCourse;

public record CourseDeleteCommand(Guid id) : IRequest<OperationResult<Guid>> { }
