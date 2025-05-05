using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.AssignmentDtos;
using Mini_Moodle.Models.Dto.SubmissionDtos;

namespace Mini_Moodle.Repositories.UserService.Queries.GetAllUserSubmissions
{
    public record UserGetSubmissionQuery : IRequest<OperationResult<List<SubmissionResponseForUserDto>>> { }
}
