using Microsoft.IdentityModel.Tokens;
using Mini_Moodle.Models.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mini_Moodle.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(ApplicationUser user, List<string> roles)
    {
        var claims = new List<Claim>
        {

        };

        return "";
    }
}
