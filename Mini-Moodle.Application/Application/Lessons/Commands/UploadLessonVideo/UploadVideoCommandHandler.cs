using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Repositories.LessonService.Interfaces;

namespace Mini_Moodle.Repositories.LessonService.Commands.UploadLessonVideo
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, OperationResult<UploadVideoResponseDto>>
    {
        private readonly ILessonCommandRepository lessonCommand;

        public UploadVideoCommandHandler(ILessonCommandRepository lessonCommand)
        {
            this.lessonCommand = lessonCommand;
        }

        public async Task<OperationResult<UploadVideoResponseDto>> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            var commandResult =  await  lessonCommand.UploadFileAsync(request.id, request.file, cancellationToken);

            return commandResult.IsSuccess ?
                  OperationResult<UploadVideoResponseDto>.Success(new UploadVideoResponseDto
                 {
                      VideoUrl = commandResult.Data!,
                      LessonId = request.id,
                     Message = "Video uploaded successfully"
                 }):
                    OperationResult<UploadVideoResponseDto>.Failure(commandResult.ErrorMessage!);


        }
    }
}
