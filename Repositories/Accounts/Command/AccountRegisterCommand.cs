using MediatR;
using Mini_Moodle.Models.Dto;

namespace Mini_Moodle.Repositories.Accounts.Command
{
    public record AccountRegisterCommand(RegisterRequestDto registerRequest) : IRequest<string?> { }
}
