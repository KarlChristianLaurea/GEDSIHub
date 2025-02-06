using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GADApplication.Data;
using GADApplication.Models;

namespace GADApplication.Pages.GARPage
{
    public class GPBModel : PageModel
    {
        private readonly GADApplication.Data.ApplicationDbContext _context;

        public GPBModel(GADApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GPB> GPB { get;set; } = default!;

        public async Task OnGetAsync()
        {
            GPB = await _context.GPBs.ToListAsync();
        }
    }
}
