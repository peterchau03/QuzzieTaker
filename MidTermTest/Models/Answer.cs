namespace QuizzieTaker.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool isCorrect { get; set; }

        //foreign key
        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
