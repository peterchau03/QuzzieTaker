using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizzieTaker.Data;
using QuizzieTaker.Models;

namespace QuizzieTaker.Pages
{
    public class Question_ManagementModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public SelectList QuestionOptions { get; set; }

        [BindProperty]
        public List<Question> Questions { get; set; }

        [BindProperty]
        public int QuestionOptionsId { get; set; } = -1; // -1 for creating new question

        [BindProperty]
        public Question SelectedQuestion { get; set; }

        [BindProperty]
        public List<Answer> Answer { get; set; } = new List<Answer>
        {
            new Answer(), // First answer
            new Answer(), // Second answer
            new Answer(),  // Third answer
            new Answer()  // Third answer
        };


        public Question_ManagementModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostLoadQuestionsAsync()
        {
            await LoadQuestions();

            if (QuestionOptionsId == -1) // Create new
            {
                SelectedQuestion = new Question();
                // Ensure we always have 3 answers
                Answer = new List<Answer> { new(), new(), new(), new() };
            }
            else // Load existing
            {
                SelectedQuestion = await _context.Questions
                    .Include(q => q.Answers)
                    .FirstOrDefaultAsync(q => q.Id == QuestionOptionsId);

                if (SelectedQuestion != null)
                {
                    Answer = SelectedQuestion.Answers.ToList();
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {

            if (QuestionOptionsId == -1)
            {
                _context.Questions.Add(SelectedQuestion);
            }
            else
            {
                _context.Questions.Update(SelectedQuestion);
            }

            foreach (var answer in Answer)
            {
                if (string.IsNullOrEmpty(answer.Text)) continue;

                if (answer.Id == 0)
                {
                    answer.QuestionId = SelectedQuestion.Id;
                    _context.Answers.Add(answer);
                }
                else
                {
                    _context.Answers.Update(answer);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }


        public async Task LoadQuestions()
        {
            Questions = await _context.Questions
                .Include(q => q.Answers)
                .OrderBy(q => q.Id)
                .ToListAsync();

            var options = Questions.Select(q => new { q.Id, Text = $"{q.Id}: {q.Description}" }).ToList();

            // Add "Create New" option at the beginning
            options.Insert(0, new { Id = -1, Text = "* Create New Question" });

            QuestionOptions = new SelectList(options, "Id", "Text", QuestionOptionsId);
        }

        public async Task OnGetAsync()
        {
            await LoadQuestions();
        }


        //public void OnGet()
        //{
        //}
    }
}
