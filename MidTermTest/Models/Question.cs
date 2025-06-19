namespace QuizzieTaker.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
