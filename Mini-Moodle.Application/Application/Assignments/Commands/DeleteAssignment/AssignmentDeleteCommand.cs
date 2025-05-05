using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.AssignmentDto;

namespace Mini_Moodle.Repositories.AssignmentsService.Commands.DeleteAssignment;

public record AssignmentDeleteCommand(Guid id) : IRequest<OperationResult<Guid>> { }  
