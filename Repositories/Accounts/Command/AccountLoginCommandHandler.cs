using MediatR;
using Microsoft.AspNetCore.Identity;
using Mini_Moodle.Authentication;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Identity;

namespace Mini_Moodle.Repositories.Accounts.Command;

public class AccountLoginCommandHandler : IRequestHandler<AccountLoginCommand, OperationResult<string>>
{
    private readonly Moodle_DbContext dbContext;

    private readonly UserManager<ApplicationUser> userManager;
    private readonly IJwtTokenGenerator jwtToken;
    private readonly IHttpContextAccessor httpContext;
    private readonly IConfiguration configuration;

    public AccountLoginCommandHandler(Moodle_DbContext dbContext, UserManager<ApplicationUser> userManager,
         IJwtTokenGenerator jwtToken, IHttpContextAccessor httpContext,
        IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
        this.jwtToken = jwtToken;
        this.httpContext = httpContext;
        this.configuration = configuration;
    }

    public async Task<OperationResult<string>> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.loginRequest.Email);

        if (user == null)
        {
            return OperationResult<string>.Failure("Incorect email or password");
        }
        var result = await userManager.CheckPasswordAsync(user, request.loginRequest.Password);

        if (result == false)
        {
            return OperationResult<string>.Failure("Incorect email or password");
        }

        var roles = await userManager.GetRolesAsync(user);

        if (roles == null || roles.Count == 0)
        {
            return OperationResult<string>.Failure("User has no roles");
        }

        httpContext.HttpContext.Response.Cookies.Append("Some-Token", jwtToken.GenerateToken(user, roles), new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["JwtToken:expire_minutes"]))
        });

        return  OperationResult<string>.Success("User successfuly log in");
    }
}
