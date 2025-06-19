using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzieTaker.Models;

namespace QuizzieTaker.Data.Configuration
{
    public class QuestionConfiguration: IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(s => s.Description)
                .IsRequired();

            builder.HasMany(m => m.Answers).WithOne(m => m.Question).HasForeignKey(m => m.QuestionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
