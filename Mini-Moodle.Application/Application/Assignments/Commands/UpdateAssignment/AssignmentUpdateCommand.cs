using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.AssignmentDto;
using Mini_Moodle.Models.Dto.AssignmentDtos;

namespace Mini_Moodle.Repositories.AssignmentsService.Commands.UpdateAssignment
{
    public record AssignmentUpdateCommand(Guid id, AssignmentRequestToUpdateDto userRequest) : IRequest<OperationResult<AssignmentResponseDto>> { }
}
