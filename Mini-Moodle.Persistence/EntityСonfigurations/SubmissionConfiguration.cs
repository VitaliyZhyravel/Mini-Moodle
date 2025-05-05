using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_Moodle.Models.Domain;
using System.Reflection.Emit;

namespace Mini_Moodle.Сonfigurations
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.User)
            .WithMany(u => u.Submissions)
            .HasForeignKey(s => s.UserId);

            builder
            .HasOne(s => s.Assignment)
            .WithMany(a => a.Submissions) 
            .HasForeignKey(s => s.AssignmentId);

        }
    }
}

