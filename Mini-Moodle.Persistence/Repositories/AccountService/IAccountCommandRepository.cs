using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Account;

namespace Mini_Moodle.Repositories.Account
{
    public interface IAccountCommandRepository
    {
        Task<OperationResult<string>> Login(LoginRequestDto loginRequest,CancellationToken cancellationToken);
        Task<OperationResult<string>> Register(RegisterRequestDto registerRequest,CancellationToken cancellationToken);
    }
}
