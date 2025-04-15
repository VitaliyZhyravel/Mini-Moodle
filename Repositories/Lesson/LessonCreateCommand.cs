using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.Lesson;

public record LessonCreateCommand(LessonRequestToCreateDto request) : IRequest<OperationResult<LessonResponseToCreateUpdateDto>> { }
