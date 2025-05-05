using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.LessonService.Commands.DeleteLesson;

public record LessonDeleteCommand(Guid id) : IRequest<OperationResult<Guid>> { };

  
    

