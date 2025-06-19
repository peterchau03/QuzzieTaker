using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizzieTaker.Data;
using QuizzieTaker.Models;

namespace QuizzieTaker.Pages
{
    public class QuizModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public List<Question> questions { get; set; }

        [BindProperty]
        public List<int> selectedAnswers { get; set; }


        [BindProperty]
        public string testTakerName { get; set; }

        [BindProperty]
        public int testTakerScore { get; set; }

        public bool questionsLoaded { get; set; } = false;

        [BindProperty]
        public List<Answer> Answer { get; set; } = new List<Answer>
        {
            new Answer(), // First answer
            new Answer(), // Second answer
            new Answer(),  // Third answer
            new Answer()  // Third answer
        };


        public QuizModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LoadQuestions()
        {
            questions = await _context.Questions.Include(q => q.Answers).OrderBy(q => q.Id).ToListAsync();
        }
        public async Task OnGetAsync()
        {
            // Initialize empty lists
            questions = new List<Question>();
            selectedAnswers = new List<int>();
        }

        public async Task<IActionResult> OnPostLoadQuestionsAsync()
        {
            if (!string.IsNullOrEmpty(testTakerName))
            {
                TempData["TestTakerName"] = testTakerName;
                questions = await _context.Questions
                    .Include(q => q.Answers)
                    .OrderBy(q => q.Id)
                    .ToListAsync();

                selectedAnswers = new List<int>(new int[questions.Count]);
                questionsLoaded = true;
            }
            else
            {
                ModelState.AddModelError("TestTakerName", "Please enter your name");
            }

            return Page();
        }


        public async Task<IActionResult> OnPostSubmitAsync()
        {
            testTakerName = TempData["TestTakerName"]?.ToString();
            Console.WriteLine($"Submitting for: {testTakerName}"); // Debug
            await LoadQuestions();

            Console.WriteLine($"Loaded {questions?.Count} questions"); // Debug
            Console.WriteLine($"Selected answers count: {selectedAnswers?.Count}"); // Debug

            int correctAnswers = 0;
            for (int i = 0; i < questions.Count; i++) { 
                var question = questions[i];

                var correctAnswer = question.Answers.FirstOrDefault(a => a.isCorrect);

                if (correctAnswer != null && selectedAnswers[i] == correctAnswer.Id) {
                    correctAnswers++;
                }
            }

            testTakerScore = (int)Math.Round((double)correctAnswers / questions.Count * 100);

            Console.WriteLine($"Calculated score: {testTakerScore}"); // Debug
            Console.WriteLine("Attempting to save score..."); // Debug
            if (testTakerName != null)
            {
                var scoreBoard = new ScoreBoard
                {
                    Score = testTakerScore,
                    Name = testTakerName,
                };

                _context.ScoreBoards.Add(scoreBoard);
                var result = await _context.SaveChangesAsync();

                Console.WriteLine($"Save result: {result} records affected"); // Debug
            }

            // Maintain the loaded state
            questionsLoaded = false;
            return Page();
        }

    }
}
