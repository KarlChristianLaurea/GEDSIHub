using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GADApplication.Data;
using GADApplication.Models;

namespace GADApplication.Pages.GADO.MandatePage
{
    public class EditModel : PageModel
    {
        private readonly GADApplication.Data.ApplicationDbContext _context;

        public EditModel(GADApplication.Data.ApplicationDbContext context)
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

            var mandate =  await _context.Mandates.FirstOrDefaultAsync(m => m.Id == id);
            if (mandate == null)
            {
                return NotFound();
            }
            Mandate = mandate;
           ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Mandate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MandateExists(Mandate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MandateExists(int id)
        {
            return _context.Mandates.Any(e => e.Id == id);
        }
    }
}
