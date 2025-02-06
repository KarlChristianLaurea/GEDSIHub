using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GADApplication.Pages.GARPage
{
    public class FileListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FileListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<GPB> GPBSubmissions { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            // Fetch only GPBs that have at least one associated file in any of their activities
            var gpbSubmissionsQuery = _context.GPBs
                .Where(gpb => gpb.Activities.Any(activity => activity.Files.Any()));

            // Apply sorting based on the sortOrder parameter
            switch (sortOrder)
            {
                case "year_asc":
                    gpbSubmissionsQuery = gpbSubmissionsQuery.OrderBy(g => g.Year);
                    break;
                case "year_desc":
                    gpbSubmissionsQuery = gpbSubmissionsQuery.OrderByDescending(g => g.Year);
                    break;
                case "responsible_asc":
                    gpbSubmissionsQuery = gpbSubmissionsQuery.OrderBy(g => g.ResponsibleUnit);
                    break;
                case "responsible_desc":
                    gpbSubmissionsQuery = gpbSubmissionsQuery.OrderByDescending(g => g.ResponsibleUnit);
                    break;
                default:
                    gpbSubmissionsQuery = gpbSubmissionsQuery.OrderByDescending(g => g.Year); // Default sort by Year descending
                    break;
            }

            GPBSubmissions = await gpbSubmissionsQuery.ToListAsync();
        }
    }
}
