using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GADApplication.Pages.GADO.AvailableMandate
{
    public class IndexUserModel : PageModel
    {
        private readonly GADApplication.Data.ApplicationDbContext _context;

        public IndexUserModel(GADApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AvailableMandates> AvailableMandates { get; set; } = default!;

        public async Task OnGetAsync()
        {
            AvailableMandates = await _context.AvailableMandates.ToListAsync();
        }
    }
}
