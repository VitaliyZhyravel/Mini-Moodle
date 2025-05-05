using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.LessonService.Commands.UpdateLesson;

public record LessonUpdateCommand(Guid id, LessonRequestToCreateDto userRequest) : IRequest<OperationResult<LessonResponseToCreateUpdateDto>> { }