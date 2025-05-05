using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.SubmissionDtos;
using Mini_Moodle.Repositories.SubmissonService.Interfaces;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.UploadVideoSubmisson;

public class SubmissionUploadFilesCommandHandler : IRequestHandler<SubmissionUploadFilesCommand, OperationResult<UploadSubmissionFileDto>>
{
    private readonly ISubmissionCommandRepository submissionCommand;

    public SubmissionUploadFilesCommandHandler(ISubmissionCommandRepository submissionCommand)
    {
        this.submissionCommand = submissionCommand;
    }

    public async Task<OperationResult<UploadSubmissionFileDto>> Handle(SubmissionUploadFilesCommand request, CancellationToken cancellationToken)
    {

        var commandResult = await submissionCommand.UploadFileAsync(request.id, request.userFile.Files, cancellationToken);

        return commandResult.IsSuccess ?
              OperationResult<UploadSubmissionFileDto>.Success(new UploadSubmissionFileDto
              {
                  FileUrl = commandResult.Data!,
                  SubmissonId = request.id,
                  Message = "Video uploaded successfully"
              }) :
                OperationResult<UploadSubmissionFileDto>.Failure(commandResult.ErrorMessage!);
    }
}


