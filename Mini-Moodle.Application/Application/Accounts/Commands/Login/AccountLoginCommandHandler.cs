using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Repositories.Account;

namespace Mini_Moodle.Repositories.AccountService.Command.Login;

public class AccountLoginCommandHandler : IRequestHandler<AccountLoginCommand, OperationResult<string>>
{
    private readonly IAccountCommandRepository account;

    public AccountLoginCommandHandler(IAccountCommandRepository account)
    {
        this.account = account;
    }

    public async Task<OperationResult<string>> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
    {
        return await account.Login(request.loginRequest, cancellationToken);
    }
}
