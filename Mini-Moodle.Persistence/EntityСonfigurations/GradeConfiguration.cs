using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Сonfigurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(g => g.Submission)
            .WithOne(s => s.Grade)
            .HasForeignKey<Grade>(g => g.SubmissionId);
        }
    }
}

