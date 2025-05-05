using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Repositories.LessonService.Interfaces;

namespace Mini_Moodle.Repositories.LessonService.Commands.DeleteLesson;

public class LessonDeleteCommandHandler : IRequestHandler<LessonDeleteCommand, OperationResult<Guid>>
{
    private readonly ILessonCommandRepository lessonCommand;

    public LessonDeleteCommandHandler(ILessonCommandRepository lessonCommand)
    {
        this.lessonCommand = lessonCommand;
    }

    public async Task<OperationResult<Guid>> Handle(LessonDeleteCommand request, CancellationToken cancellationToken)
    {
        var commandResult = await lessonCommand.DeleteAsync(request.id, cancellationToken);

        return commandResult.IsSuccess
            ? OperationResult<Guid>.Success(commandResult.Data!)
            : OperationResult<Guid>.Failure(commandResult.ErrorMessage!);
    }
}
