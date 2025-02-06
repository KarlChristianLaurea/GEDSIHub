using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GADApplication.Pages.GPBPage
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Define GPBs property
        public IList<GPB> GPBs { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Load the GPBs list asynchronously with sorting options
        public async Task OnGetAsync(string sortOrder)
        {
            // Determine sorting order
            var gpbs = from g in _context.GPBs
                       select g;

            switch (sortOrder)
            {
                case "year_desc":
                    gpbs = gpbs.OrderByDescending(g => g.Year);
                    break;
                case "year_asc":
                    gpbs = gpbs.OrderBy(g => g.Year);
                    break;
                case "responsible_asc":
                    gpbs = gpbs.OrderBy(g => g.ResponsibleUnit);
                    break;
                case "responsible_desc":
                    gpbs = gpbs.OrderByDescending(g => g.ResponsibleUnit);
                    break;
                default:
                    gpbs = gpbs.OrderBy(g => g.Year); // Default sort by Year ascending
                    break;
            }

            GPBs = await gpbs.AsNoTracking().ToListAsync();
        }
    }
}
