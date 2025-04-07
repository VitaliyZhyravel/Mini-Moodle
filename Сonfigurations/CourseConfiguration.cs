using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Сonfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(l => l.Id);

            builder.HasOne(l => l.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.CourseId);

            builder.HasMany(l => l.Assignments)
                .WithOne(a => a.Lesson)
                .HasForeignKey(a => a.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Lesson)
                .WithMany(l => l.Assignments)
                .HasForeignKey(a => a.LessonId);
        }
    }
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.User)
            .WithMany(u => u.Submissions) 
            .HasForeignKey(g => g.UserId);

            builder.HasOne(s => s.Assignment)
           .WithOne() 
           .HasForeignKey<Submission>(s => s.AssignmentId);
        }
    }
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasOne(g => g.Submission)
            .WithOne()
            .HasForeignKey<Grade>(g => g.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}

