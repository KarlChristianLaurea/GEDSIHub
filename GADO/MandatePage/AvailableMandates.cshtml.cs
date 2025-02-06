using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using GADApplication.Data; // Assuming this is where your DbContext is located
using GADApplication.Models; // Assuming this is where your Mandate model is located

namespace GADApplication.Pages.GADO.MandatePage
{
    public class AvailableMandatesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AvailableMandatesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Mandate> Mandates { get; set; }

        // Fetch the mandates when the page loads
        public async Task OnGetAsync()
        {
            // Fetch the mandates from the database
            Mandates = await _context.Mandates.ToListAsync();
        }
    }
}
