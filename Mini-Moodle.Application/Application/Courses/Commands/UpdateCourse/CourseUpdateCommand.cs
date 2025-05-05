using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Course;

namespace Mini_Moodle.Repositories.CourseService.Commands.UpdateCourse;

public record CourseUpdateCommand(Guid id, CourseRequestToUpdateDto request) : IRequest<OperationResult<CourseResponseCreateUpdateDto>> { }

