using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.LessonService.Queries.GetByIdLesson;

public record LessonGetByIdQuery(Guid id) : IRequest<OperationResult<LessonResponseDto>> { }