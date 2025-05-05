using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Repositories.GradeService.Interfaces
{
    public interface IGradeCommandRepository
    {
        Task<OperationResult<Grade>> CreateAsync(Grade request, CancellationToken cancellationToken);
    }
}
