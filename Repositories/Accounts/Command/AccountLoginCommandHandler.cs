using MediatR;
using Microsoft.AspNetCore.Identity;
using Mini_Moodle.Authentication;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Models.Identity;

namespace Mini_Moodle.Repositories.Accounts.Command
{
    public class AccountLoginCommandHandler : IRequestHandler<AccountLoginCommand, string>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJwtTokenGenerator jwtToken;
        private readonly IHttpContextAccessor httpContext;
        private readonly IConfiguration configuration;

        public AccountLoginCommandHandler(Moodle_DbContext dbContext, RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtToken, IHttpContextAccessor httpContext,
            IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.jwtToken = jwtToken;
            this.httpContext = httpContext;
            this.configuration = configuration;
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

            httpContext.HttpContext.Response.Cookies.Append("Some-Token", jwtToken.GenerateToken(user, roles), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes( Convert.ToDouble(configuration["JwtToken:expire_minutes"]))
            });

            return "User secsesfully log in";
        }
    }
}
