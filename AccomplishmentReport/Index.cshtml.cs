using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GADApplication.Pages.AccomplishmentPage
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<GADApplication.Models.AccomplishmentReport> AccomplishmentReports { get; set; }

        public async Task OnGetAsync()
        {
            AccomplishmentReports = await _context.AccomplishmentReports
                .Include(r => r.ActivityAccomplishments)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var report = await _context.AccomplishmentReports.FindAsync(id);

            if (report != null)
            {
                _context.AccomplishmentReports.Remove(report);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
