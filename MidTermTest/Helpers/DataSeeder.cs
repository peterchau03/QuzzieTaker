using MidTermTest.Models;
using QuizzieTaker.Data;

namespace QuizzieTaker.Helpers
{
    public static class DataSeeder
    {
        public static void SeedQuestions(ApplicationDbContext context)
        {
            if (context.Questions.Any()) return;

            var questions = new[]
            {
                new Question
                {
                    Description = "What is the capital of Australia?",
                    Answers = new[]
                    {
                        new Answer {Text = "Melbourne" , isCorrect = false},
                        new Answer {Text = "Sydney", isCorrect = false},
                        new Answer {Text = "Canberra", isCorrect=true},
                        new Answer {Text = "Brisbane", isCorrect=false},
                    }
                },

                new Question
                {
                    Description = "What is the capital of Vietnam?",
                    Answers = new[]
                    {
                        new Answer {Text = "Ho Chi Minh City" , isCorrect = false},
                        new Answer {Text = "Hanoi", isCorrect = true},
                        new Answer {Text = "Da Nang", isCorrect=false},
                        new Answer {Text = "Hue", isCorrect=false},
                    }
                },

                new Question
                {
                    Description = "What continent is Vietnam in?",
                    Answers = new[]
                    {
                        new Answer {Text = "Asia" , isCorrect = true},
                        new Answer {Text = "Europe", isCorrect = false},
                        new Answer {Text = "Oceania", isCorrect=false},
                        new Answer {Text = "Antartica", isCorrect=false},
                    }
                },

                new Question
                {
                    Description = "Which of these is a car brand?",
                    Answers = new[]
                    {
                        new Answer {Text = "Boeing" , isCorrect = false},
                        new Answer {Text = "Airbus", isCorrect = false},
                        new Answer {Text = "Microsoft", isCorrect=false},
                        new Answer {Text = "VinFast", isCorrect=true},
                    }
                },

                new Question
                {
                    Description = "Which company created .NET?",
                    Answers = new[]
                    {
                        new Answer {Text = "Asus" , isCorrect = false},
                        new Answer {Text = "Microsoft", isCorrect = true},
                        new Answer {Text = "IBM", isCorrect=false},
                        new Answer {Text = "Apple", isCorrect=false},
                    }
                },
            };

            context.Questions.AddRange(questions);
            context.SaveChanges();
        }
    }
}
