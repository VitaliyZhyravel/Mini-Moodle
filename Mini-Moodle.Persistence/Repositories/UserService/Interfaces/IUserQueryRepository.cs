using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.SubmissionDtos;

namespace Mini_Moodle.Repositories.UserService.Interfaces
{
    public interface IUserQueryRepository
    {
        Task<OperationResult<List<Submission>>> GetSubmissionsAsync(CancellationToken cancellationToken);
    }
}
