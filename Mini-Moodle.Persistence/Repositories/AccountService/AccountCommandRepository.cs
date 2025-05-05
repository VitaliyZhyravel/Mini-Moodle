using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mini_Moodle.Authentication;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Account;
using Mini_Moodle.Models.Identity;

namespace Mini_Moodle.Repositories.Account
{
    public class AccountCommandRepository : IAccountCommandRepository
    {
        private readonly Moodle_DbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJwtTokenGenerator jwtToken;
        private readonly IHttpContextAccessor httpContext;
        private readonly IConfiguration configuration;

        public AccountCommandRepository(Moodle_DbContext dbContext,UserManager<ApplicationUser> userManager,IJwtTokenGenerator jwtToken,
            IHttpContextAccessor httpContext,IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.jwtToken = jwtToken;
            this.httpContext = httpContext;
            this.configuration = configuration;
        }

        public async Task<OperationResult<string>> Login(LoginRequestDto loginRequest,CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                return OperationResult<string>.Failure("Incorect email or password");
            }
            var result = await userManager.CheckPasswordAsync(user, loginRequest.Password);

            if (result == false)
            {
                return OperationResult<string>.Failure("Incorect email or password");
            }

            var roles = await userManager.GetRolesAsync(user);

            if (roles == null || roles.Count == 0)
            {
                return OperationResult<string>.Failure("User has no roles");
            }

            httpContext?.HttpContext?.Response.Cookies.Append("Some-Token", jwtToken.GenerateToken(user, roles), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["JwtToken:expire_minutes"]))
            });

            return OperationResult<string>.Success("Login success");
        }

        public async Task<OperationResult<string>> Register(RegisterRequestDto registerRequest, CancellationToken cancellationToken)
        {
            if (await userManager.FindByEmailAsync(registerRequest.Email) != null)
            {
                return OperationResult<string>.Failure("This Email is alredy used");
            }
            var newUser = new ApplicationUser
            {
                UserName = registerRequest.Name,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber
            };

            var isFirstUser = await dbContext.Users.AnyAsync(cancellationToken);

            var result = await userManager.CreateAsync(newUser, registerRequest.Password);

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
