using Microsoft.EntityFrameworkCore;
using QuizzieTaker.Models;
using System.Security.Cryptography.X509Certificates;

namespace QuizzieTaker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<ScoreBoard> ScoreBoards { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        protected ApplicationDbContext()
        {
        }
    }
}
