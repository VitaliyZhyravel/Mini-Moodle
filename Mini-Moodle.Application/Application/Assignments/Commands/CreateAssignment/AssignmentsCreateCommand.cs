using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.AssignmentDto;

namespace Mini_Moodle.Repositories.AssignmentsService.Commands.CreateAssignment;

public record AssignmentsCreateCommand(AssignmentRequestDto userRequest) : IRequest<OperationResult<AssignmentResponseDto>> { }
