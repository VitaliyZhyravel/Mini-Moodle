using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Repositories.CourseService.Interfaces
{
    public interface ICourseCommandRepository
    {
        Task<OperationResult<Course>> CreateAsync(Course userRequest, CancellationToken cancellationToken);
        Task<OperationResult<Course>> UpdateAsync(Guid id, Course userRequest, CancellationToken cancellationToken);
        Task<OperationResult<Guid>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
