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

namespace GADApplication.Pages.GADO.AvailableMandate
{
    public class EditModel : PageModel
    {
        private readonly GADApplication.Data.ApplicationDbContext _context;

        public EditModel(GADApplication.Data.ApplicationDbContext context)
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

            var availablemandates =  await _context.AvailableMandates.FirstOrDefaultAsync(m => m.Id == id);
            if (availablemandates == null)
            {
                return NotFound();
            }
            AvailableMandates = availablemandates;
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

            _context.Attach(AvailableMandates).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvailableMandatesExists(AvailableMandates.Id))
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

        private bool AvailableMandatesExists(int id)
        {
            return _context.AvailableMandates.Any(e => e.Id == id);
        }
    }
}
