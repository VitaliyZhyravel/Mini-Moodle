using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_Moodle.Models.Identity;

namespace Mini_Moodle.Сonfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.
            HasData(
            new ApplicationRole
            {
                Id = new Guid("1b04ab86-be5b-4fa6-a460-1951c2ae6370"),
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new ApplicationRole
            {
                Id = new Guid("eecb09e1-5951-47d0-ae4e-8c28587176d4"),
                Name = "Teacher",
                NormalizedName = "TEACHER"
            },
            new ApplicationRole
            {
                Id = new Guid("66701dba-3c42-4997-a788-12c2ee8dd384"),
                Name = "User",
                NormalizedName = "USER"
            },
            new ApplicationRole
            {
                Id = new Guid("42331567-9bd0-44df-a3ed-c41342e65298"),
                Name = "Student",
                NormalizedName = "STUDENT"
            });
    }
}
