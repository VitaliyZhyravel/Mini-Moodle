using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_Moodle.Models.Domain;

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

            builder.HasOne(s => s.Assignment)
           .WithOne() 
           .HasForeignKey<Submission>(s => s.AssignmentId);
        }
    }
}

