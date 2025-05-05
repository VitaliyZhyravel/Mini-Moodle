using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Repositories.AssignmentServices.Interfaces;

namespace Mini_Moodle.Repositories.AssignmentsService.Commands.DeleteAssignment;

public class AssignmentDeleteCommandHandler : IRequestHandler<AssignmentDeleteCommand, OperationResult<Guid>>
{
    private readonly IAssignmentCommandRepository assignmentCommand;

    public AssignmentDeleteCommandHandler(IAssignmentCommandRepository assignmentCommand)
    {
        this.assignmentCommand = assignmentCommand;
    }

    public async Task<OperationResult<Guid>> Handle(AssignmentDeleteCommand request, CancellationToken cancellationToken)
    {
        
        var commandResult = await assignmentCommand.DeleteAsync(request.id,cancellationToken);

      return  commandResult.IsSuccess ? OperationResult<Guid>.Success(commandResult.Data)
            : OperationResult<Guid>.Failure(commandResult.ErrorMessage!);
    }
}
