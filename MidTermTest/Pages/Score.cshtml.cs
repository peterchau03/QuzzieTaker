using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizzieTaker.Data;

namespace QuizzieTaker.Pages
{
    public class ScoreModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        [BindProperty]
        public List<ScoreBoard> ScoreBoards { get; set; }

        public ScoreModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            ScoreBoards = _context.ScoreBoards.OrderBy(s => s.Id).ToList();
        }
    }
}
