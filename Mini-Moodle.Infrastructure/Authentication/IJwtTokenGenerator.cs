using Mini_Moodle.Models.Identity;

namespace Mini_Moodle.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
