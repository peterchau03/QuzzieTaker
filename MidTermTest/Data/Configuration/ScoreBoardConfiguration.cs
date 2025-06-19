using MidTermTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuizzieTaker.Data.Configuration
{
    public class ScoreBoardConfiguration : IEntityTypeConfiguration<ScoreBoard>
    {
        public void Configure(EntityTypeBuilder<ScoreBoard> builder)
        {
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

            builder.Property(i => i.Score).IsRequired();
        }
    }
}
