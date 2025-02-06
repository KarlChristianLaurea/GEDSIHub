using GADApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GADApplication.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace GADApplication.Pages.GADO.SubmissionSettings
{
    public class SetGARSubmissionDatesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SetGARSubmissionDatesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DateTime SubmissionStartDate { get; set; }

        [BindProperty]
        public DateTime SubmissionEndDate { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var garSubmissionSettings = await _context.GARSubmissionSettings.FirstOrDefaultAsync();

            if (garSubmissionSettings != null)
            {
                SubmissionStartDate = garSubmissionSettings.StartDate;
                SubmissionEndDate = garSubmissionSettings.EndDate;
            }
            else
            {
                SubmissionStartDate = DateTime.Now;
                SubmissionEndDate = DateTime.Now.AddDays(7);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please correct the errors in the form.";
                return Page();
            }

            var garSubmissionSettings = await _context.GARSubmissionSettings.FirstOrDefaultAsync();

            if (garSubmissionSettings == null)
            {
                garSubmissionSettings = new GARSubmissionSettings
                {
                    StartDate = SubmissionStartDate,
                    EndDate = SubmissionEndDate
                };
                _context.GARSubmissionSettings.Add(garSubmissionSettings);
            }
            else
            {
                garSubmissionSettings.StartDate = SubmissionStartDate;
                garSubmissionSettings.EndDate = SubmissionEndDate;
            }

            await _context.SaveChangesAsync();

            SuccessMessage = "Submission dates for GAR have been successfully set.";
            return RedirectToPage("/GARPage/Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var garSubmissionSettings = await _context.GARSubmissionSettings.FirstOrDefaultAsync();

            if (garSubmissionSettings != null)
            {
                _context.GARSubmissionSettings.Remove(garSubmissionSettings);
                await _context.SaveChangesAsync();
                SuccessMessage = "Submission settings have been deleted.";
            }
            else
            {
                ErrorMessage = "No submission settings found to delete.";
            }

            return RedirectToPage("/GARPage/Index");
        }
    }
}
