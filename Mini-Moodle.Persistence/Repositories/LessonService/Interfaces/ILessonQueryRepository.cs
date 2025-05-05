using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Repositories.LessonService.Interfaces
{
    public interface ILessonQueryRepository
    {
        Task<OperationResult<List<Lesson?>>> GetAllAsync(string? filterBy, string? filterOn,
            string? sortBy, bool isAscending, int pageNumber, int pageSize,CancellationToken cancellationToken);
        Task<OperationResult<Lesson?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
