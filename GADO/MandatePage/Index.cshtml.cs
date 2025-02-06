using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GADApplication.Data;
using GADApplication.Models;

namespace GADApplication.Pages.GADO.MandatePage
{
    public class IndexModel : PageModel
    {
        private readonly GADApplication.Data.ApplicationDbContext _context;

        public IndexModel(GADApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Mandate> Mandate { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Mandate = await _context.Mandates
                .Include(m => m.Activity).ToListAsync();
        }
    }
}
