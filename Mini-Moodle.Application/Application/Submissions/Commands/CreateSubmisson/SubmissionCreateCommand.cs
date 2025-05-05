using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.SubmissionDtos;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.CreateSubmisson;

public record SubmissionCreateCommand(SubmissionRequestDto userRequest) : IRequest<OperationResult<SubmissionResponseDto>> { }

