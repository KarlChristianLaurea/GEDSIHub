using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GADApplication.Data;
using GADApplication.Models;

namespace GADApplication.Pages.GADO.AvailableMandate
{
    public class DetailsModel : PageModel
    {
        private readonly GADApplication.Data.ApplicationDbContext _context;

        public DetailsModel(GADApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public AvailableMandates AvailableMandates { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availablemandates = await _context.AvailableMandates.FirstOrDefaultAsync(m => m.Id == id);
            if (availablemandates == null)
            {
                return NotFound();
            }
            else
            {
                AvailableMandates = availablemandates;
            }
            return Page();
        }
    }
}
