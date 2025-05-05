using AutoMapper;
using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Repositories.LessonService.Interfaces;

namespace Mini_Moodle.Repositories.LessonService.Commands.CreateLesson;

public class LessonCreateCommandHandler : IRequestHandler<LessonCreateCommand, OperationResult<LessonResponseToCreateUpdateDto>>
{
    private readonly IMapper mapper;
    private readonly ILessonCommandRepository lessonCommand;

    public LessonCreateCommandHandler(IMapper mapper, ILessonCommandRepository lessonCommand)
    {
        this.mapper = mapper;
        this.lessonCommand = lessonCommand;
    }

    public async Task<OperationResult<LessonResponseToCreateUpdateDto>> Handle(LessonCreateCommand request, CancellationToken cancellationToken)
    {
        var newLesson = mapper.Map<Models.Domain.Lesson>(request.request);

        var commandResult =  await lessonCommand.CreateAsync(newLesson, cancellationToken);

        return commandResult.IsSuccess
            ? OperationResult<LessonResponseToCreateUpdateDto>.Success(mapper.Map<LessonResponseToCreateUpdateDto>(commandResult.Data))
            : OperationResult<LessonResponseToCreateUpdateDto>.Failure(commandResult.ErrorMessage!);

    }
}
