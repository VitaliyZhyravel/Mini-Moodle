using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.SubmissionDtos;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.UpdateSubmisson;

public record SubmissionUpdateCommand(Guid id,SubmissionRequestToUpdate userRequest) : IRequest<OperationResult<SubmissionResponseDto>>{ }

