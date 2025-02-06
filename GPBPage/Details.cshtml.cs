using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GADApplication.Pages.GPBPage
{
    public class DetailsModel : PageModel
    {
        
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Bind the GPB model (singular) for display
        [BindProperty]
        public GPB GPB { get; set; }

        // Method to fetch GPB along with its Activities and Mandates
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch GPB including its related Activities and Mandates
            GPB = await _context.GPBs
                .Include(g => g.Activities)
                .ThenInclude(a => a.Mandates)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (GPB == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
