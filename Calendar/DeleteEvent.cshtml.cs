using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace GADApplication.Pages.Calendar
{
    public class DeleteEventModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteEventModel(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        [BindProperty]
        public CalendarEvent EventData { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Find the event by ID
            EventData = await _dbContext.CalendarEvents.FindAsync(id);

            if (EventData == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var existingEvent = await _dbContext.CalendarEvents.FindAsync(id);

            if (existingEvent == null)
            {
                return NotFound();
            }

            // Remove the event from the database
            _dbContext.CalendarEvents.Remove(existingEvent);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/Calendar/Calendar");
        }
    }
}
