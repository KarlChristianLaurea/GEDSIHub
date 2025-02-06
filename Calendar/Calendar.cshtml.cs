using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GADApplication.Pages.Calendar
{
    public class CalendarModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CalendarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CalendarEvent> UpcomingEvents { get; set; }

        // This method fetches the list of events to display in the calendar
        public async Task OnGetAsync()
        {
            // Fetch upcoming events from the database and pass them to the view
            UpcomingEvents = await _context.CalendarEvents.ToListAsync();
        }
    }
}