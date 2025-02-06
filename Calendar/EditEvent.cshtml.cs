using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace GADApplication.Pages.Calendar
{
    public class EditEventModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public EditEventModel(ApplicationDbContext context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingEvent = await _dbContext.CalendarEvents.FindAsync(id);
            if (existingEvent == null)
            {
                return NotFound();
            }

            // Update event details
            existingEvent.Name = EventData.Name;
            existingEvent.Description = EventData.Description;
            existingEvent.Location = EventData.Location;
            existingEvent.Attendees = EventData.Attendees;
            existingEvent.IsConfirmed = EventData.IsConfirmed;
            existingEvent.Start = EventData.Start;
            existingEvent.End = EventData.End;

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/Calendar/Calendar");
        }
    }
}