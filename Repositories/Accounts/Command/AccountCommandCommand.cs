using MediatR;
using Mini_Moodle.Models.Dto;

namespace Mini_Moodle.Repositories.Accounts.Command
{
    public record AccountCommandCommand(RegisterRequestDto registerRequest) : IRequest<string?> { }
}
