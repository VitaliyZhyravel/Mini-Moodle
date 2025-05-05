using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Repositories.AssignmentServices.Interfaces;

public interface IAssignmentCommandRepository
{
    Task<OperationResult<Assignment>> CreateAsync(Assignment userRequest, CancellationToken cancellationToken);
    Task<OperationResult<Assignment>> UpdateAsync(Guid id, Assignment userRequest, double AddMinutesToDeadLine, CancellationToken cancellationToken);
    Task<OperationResult<Guid>> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
