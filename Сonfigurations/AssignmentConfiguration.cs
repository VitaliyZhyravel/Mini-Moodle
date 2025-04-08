using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Сonfigurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Lesson)
                .WithMany(l => l.Assignments)
                .HasForeignKey(a => a.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

