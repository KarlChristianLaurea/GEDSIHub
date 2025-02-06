using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using GADApplication.Data;
using GADApplication.Models;

namespace GADApplication.Pages.Calendar
{
    public class UserCalendarModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UserCalendarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CalendarEvent> UpcomingEvents { get; set; }

        // Fetches all events to display them to all users
        public async Task OnGetAsync()
        {
            // Retrieve all events set by the admin
            UpcomingEvents = await _context.CalendarEvents.ToListAsync();
        }
    }
}
