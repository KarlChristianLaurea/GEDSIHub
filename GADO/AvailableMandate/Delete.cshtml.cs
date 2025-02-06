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
    public class DeleteModel : PageModel
    {
        private readonly GADApplication.Data.ApplicationDbContext _context;

        public DeleteModel(GADApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availablemandates = await _context.AvailableMandates.FindAsync(id);
            if (availablemandates != null)
            {
                AvailableMandates = availablemandates;
                _context.AvailableMandates.Remove(AvailableMandates);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
