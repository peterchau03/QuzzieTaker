using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzieTaker.Models;

namespace QuizzieTaker.Data.Configuration
{
    public class AnswerConfiguration: IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder) {
            builder.Property(m => m.Text).IsRequired();

            builder.Property(b => b.isCorrect).IsRequired();

            builder.HasOne(q => q.Question).WithMany(a => a.Answers).HasForeignKey(q => q.QuestionId).OnDelete(DeleteBehavior.Cascade); 


        }
    }
}
