using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Repositories.AssignmentServices.Interfaces
{
    public interface IAssignmentQueryRepository
    {
        Task<OperationResult<Assignment>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<List<Assignment>>> GetAllAsync(string? filterBy, string? filterOn,
            string? sortBy, bool isAscending, int pageNumber, int pageSize, CancellationToken cancellationToken);
        //Task<OperationResult<IEnumerable<Assignment>>> GetByLessonIdAsync(Guid lessonId, CancellationToken cancellationToken);
        //Task<OperationResult<IEnumerable<Assignment>>> GetByDifficultyIdAsync(Guid difficultyId, CancellationToken cancellationToken);
        //Task<OperationResult<IEnumerable<Assignment>>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    }
}
