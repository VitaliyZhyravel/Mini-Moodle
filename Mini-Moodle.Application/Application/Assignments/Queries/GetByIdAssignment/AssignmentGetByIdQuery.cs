using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.AssignmentDto;

namespace Mini_Moodle.Repositories.AssignmentsService.Queries.GetByIdAssignment;

public record AssignmentGetByIdQuery(Guid id) : IRequest<OperationResult<AssignmentResponseDto>> { }

