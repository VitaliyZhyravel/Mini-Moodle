using Microsoft.AspNetCore.Identity;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<Submission> Submissions { get; set; }
    }
}
