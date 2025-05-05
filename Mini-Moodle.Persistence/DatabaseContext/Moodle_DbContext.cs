using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Identity;
using Mini_Moodle.Сonfigurations;
using System.Reflection;

namespace Mini_Moodle.DatabaseContext
{
    public class Moodle_DbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public Moodle_DbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
