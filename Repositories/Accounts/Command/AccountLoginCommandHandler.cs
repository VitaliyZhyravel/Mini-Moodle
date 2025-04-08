using MediatR;
using Microsoft.AspNetCore.Identity;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Models.Identity;

namespace Mini_Moodle.Repositories.Accounts.Command
{
    public class AccountLoginCommandHandler : IRequestHandler<AccountLoginCommand, string>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountLoginCommandHandler(Moodle_DbContext dbContext, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<string> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.loginRequest.Email);

            if (user == null)
            {
                return "Incorect email or password";
            }
            var result = await userManager.CheckPasswordAsync(user, request.loginRequest.Password);

            if (result == false) 
            {
                return "Incorect email or password";
            }

            var roles = await userManager.GetRolesAsync(user);

            if (roles == null || roles.Count == 0)
            {
                return "User has no roles";
            }

            return "";

        }
    }
}
