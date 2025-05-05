using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Account;

namespace Mini_Moodle.Repositories.AccountService.Command.Register;

public record AccountRegisterCommand(RegisterRequestDto registerRequest) : IRequest<OperationResult<string>> { }
