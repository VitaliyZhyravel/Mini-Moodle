using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Repositories.CourseService.Interfaces
{
    public interface ICourseQueryRepository
    {
        Task<OperationResult<List<Course?>>> GetAllAsync(string? filterBy, string? filterOn,
            string? sortBy, bool isAscending, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<OperationResult<Course?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
