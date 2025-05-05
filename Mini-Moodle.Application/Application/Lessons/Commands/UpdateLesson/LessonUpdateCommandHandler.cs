using AutoMapper;
using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Repositories.LessonService.Interfaces;

namespace Mini_Moodle.Repositories.LessonService.Commands.UpdateLesson;

public class LessonUpdateCommandHandler : IRequestHandler<LessonUpdateCommand, OperationResult<LessonResponseToCreateUpdateDto>>
{
    private readonly IMapper mapper;
    private readonly ILessonCommandRepository lessonCommand;

    public LessonUpdateCommandHandler( IMapper mapper,ILessonCommandRepository lessonCommand)
    {
        this.mapper = mapper;
        this.lessonCommand = lessonCommand;
    }
    public async Task<OperationResult<LessonResponseToCreateUpdateDto>> Handle(LessonUpdateCommand request, CancellationToken cancellationToken)
    {
        var resultCommand = await lessonCommand.UpdateAsync(request.id,mapper.Map<Lesson>(request.userRequest), cancellationToken);

        return resultCommand.IsSuccess ? OperationResult<LessonResponseToCreateUpdateDto>.Success(mapper.Map<LessonResponseToCreateUpdateDto>(resultCommand.Data)) :
            OperationResult<LessonResponseToCreateUpdateDto>.Failure(resultCommand.ErrorMessage);
    }
}
