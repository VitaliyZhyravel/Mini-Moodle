using MediatR;
using Microsoft.AspNetCore.Identity;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Models.Identity;

namespace Mini_Moodle.Repositories.Accounts.Command
{
    public class AccountCommandCommandHandler : IRequestHandler<AccountCommandCommand, string?>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountCommandCommandHandler(Moodle_DbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<string?> Handle(AccountCommandCommand request, CancellationToken cancellationToken)
        {
            if (await userManager.FindByEmailAsync(request.registerRequest.Email) != null)
            {
                return "Something goes wrong";
            }
            var newUser = new ApplicationUser
            {
                UserName = request.registerRequest.Name,
                Email = request.registerRequest.Email,
                PhoneNumber = request.registerRequest.PhoneNumber
            };
            var result = await userManager.CreateAsync(newUser, request.registerRequest.Password);

            if (!result.Succeeded) 
            {
                return "Something goes wrong";
            }
            
            var resultRole = await userManager.AddToRoleAsync(newUser, "Student");

            if (!resultRole.Succeeded)
            {
                return "Something goes wrong";
            }
            return "Registration successful";
        }
    }
}
