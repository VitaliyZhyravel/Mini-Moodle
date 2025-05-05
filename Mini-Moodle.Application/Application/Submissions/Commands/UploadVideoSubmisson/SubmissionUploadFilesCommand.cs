using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Models.Dto.SubmissionDtos;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.UploadVideoSubmisson;

public record SubmissionUploadFilesCommand(Guid id, SubmissionFileUploadDto userFile) : IRequest<OperationResult<UploadSubmissionFileDto>> { }

