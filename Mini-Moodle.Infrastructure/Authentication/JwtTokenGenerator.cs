using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mini_Moodle.Models.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mini_Moodle.Authentication;

public class JwtTokenGenerator(IConfiguration configuration) : IJwtTokenGenerator
{    public string GenerateToken(ApplicationUser user, IList<string> roles)
    {
        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Унікальний ідентифікатор користувача
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Унікальний  ідентифікатор токена
                new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64), // Час створення токена в секундах з 1 січня 1970р
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Ідентифікатор користувача
                new Claim(ClaimTypes.Email, user.Email), // Email користувача
                new Claim(ClaimTypes.Name, user.UserName) // Ім'я користувача
            };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        // Перетворює наш ключ в байти
        SymmetricSecurityKey keyInBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtToken:key"]));

        // Хешує наш ключ
        SigningCredentials hashKey = new SigningCredentials(keyInBytes, SecurityAlgorithms.HmacSha256);

        // Створює наш токен
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: configuration["JwtToken:issuer"],
            audience: configuration["JwtToken:audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes( Convert.ToDouble(configuration["JwtToken:expire_minutes"])),
            signingCredentials: hashKey
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
