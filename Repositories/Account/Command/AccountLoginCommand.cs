using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Account;

namespace Mini_Moodle.Repositories.Accounts.Command;

public record AccountLoginCommand(LoginRequestDto loginRequest) : IRequest<OperationResult<string>> { }
