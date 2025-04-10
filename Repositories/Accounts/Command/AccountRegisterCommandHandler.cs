using MediatR;
using Microsoft.AspNetCore.Identity;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Models.Identity;

namespace Mini_Moodle.Repositories.Accounts.Command
{
    public class AccountRegisterCommandHandler : IRequestHandler<AccountRegisterCommand, string?>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountRegisterCommandHandler(Moodle_DbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<string?> Handle(AccountRegisterCommand request, CancellationToken cancellationToken)
        {
            if (await userManager.FindByEmailAsync(request.registerRequest.Email) != null)
            {
                return "This Email is alredy used";
            }
            var newUser = new ApplicationUser
            {
                UserName = request.registerRequest.Name,
                Email = request.registerRequest.Email,
                PhoneNumber = request.registerRequest.PhoneNumber
            };

            var isFirstUser = dbContext.Users.Any();

            var result = await userManager.CreateAsync(newUser, request.registerRequest.Password);

            if (!result.Succeeded) 
            {
                return "Something goes wrong";
            }
            
            var resultRole = isFirstUser ? await userManager.AddToRoleAsync(newUser, "User") : await userManager.AddToRoleAsync(newUser, "Admin");

            if (!resultRole.Succeeded)
            {
                return "Something goes wrong";
            }
            return "Registration successful";
        }
    }
}
