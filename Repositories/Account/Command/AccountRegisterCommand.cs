using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Account;

namespace Mini_Moodle.Repositories.Accounts.Command;

public record AccountRegisterCommand(RegisterRequestDto registerRequest) : IRequest<OperationResult<string>> { }
