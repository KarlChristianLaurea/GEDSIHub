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
    public class DeleteModel : PageModel
    {
        private readonly GADApplication.Data.ApplicationDbContext _context;

        public DeleteModel(GADApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mandate Mandate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mandate = await _context.Mandates.FirstOrDefaultAsync(m => m.Id == id);

            if (mandate == null)
            {
                return NotFound();
            }
            else
            {
                Mandate = mandate;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mandate = await _context.Mandates.FindAsync(id);
            if (mandate != null)
            {
                Mandate = mandate;
                _context.Mandates.Remove(Mandate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
