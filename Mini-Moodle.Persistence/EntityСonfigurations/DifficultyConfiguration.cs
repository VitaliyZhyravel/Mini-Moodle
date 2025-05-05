using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Сonfigurations;

public class DifficultyConfiguration : IEntityTypeConfiguration<Difficulty>
{
    public void Configure(EntityTypeBuilder<Difficulty> builder)
    {
       builder.HasData(
            new Difficulty
            {
                Id = Guid.Parse("59483b19-5283-4800-9ab9-07cbdd37aa19"),
                Name = "Easy",
                DaysToExpire = 2
            },
            new Difficulty
            {
                Id = Guid.Parse("53e0a3bf-00f0-4bbb-a789-9fd2fe7cf50c"),
                Name = "Medium",
                DaysToExpire = 5
            },
            new Difficulty
            {
                Id = Guid.Parse("5346433e-8902-40f2-8dde-03680bd679e3"),
                Name = "Hard",
                DaysToExpire = 8
            }
        );
    }
}
