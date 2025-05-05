using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Identity;
using Mini_Moodle.Repositories.Account;

namespace Mini_Moodle.Repositories.AccountService.Command.Register
{
    public class AccountRegisterCommandHandler : IRequestHandler<AccountRegisterCommand, OperationResult<string>>
    {
        private readonly IAccountCommandRepository account;

        public AccountRegisterCommandHandler(IAccountCommandRepository account)
        {
            this.account = account;
        }
        public async Task<OperationResult<string>> Handle(AccountRegisterCommand request, CancellationToken cancellationToken)
        {
            return await account.Register(request.registerRequest,cancellationToken);
        }
    }
}
