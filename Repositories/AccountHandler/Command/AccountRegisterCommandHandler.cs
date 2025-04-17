using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Identity;

namespace Mini_Moodle.Repositories.Accounts.Command
{
    public class AccountRegisterCommandHandler : IRequestHandler<AccountRegisterCommand, OperationResult<string>>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountRegisterCommandHandler(Moodle_DbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<OperationResult<string>> Handle(AccountRegisterCommand request, CancellationToken cancellationToken)
        {
            if (await userManager.FindByEmailAsync(request.registerRequest.Email) != null)
            {
                return OperationResult<string>.Failure("This Email is alredy used");
            }
            var newUser = new ApplicationUser
            {
                UserName = request.registerRequest.Name,
                Email = request.registerRequest.Email,
                PhoneNumber = request.registerRequest.PhoneNumber
            };

            var isFirstUser = await  dbContext.Users.AnyAsync(cancellationToken);

            var result = await userManager.CreateAsync(newUser, request.registerRequest.Password);

            if (!result.Succeeded)
            {
                return OperationResult<string>.Failure("Something goes wrong");
            }

            var resultRole = isFirstUser ? await userManager.AddToRoleAsync(newUser, "User") : await userManager.AddToRoleAsync(newUser, "Admin");

            if (!resultRole.Succeeded)
            {
                return OperationResult<string>.Failure("Something goes wrong");
            }
            return OperationResult<string>.Success("Registration successful");
        }
    }
}
