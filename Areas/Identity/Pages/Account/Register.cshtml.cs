// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using GADApplication.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GADApplication.Data; // Ensure you are importing your DbContext

namespace GADApplication.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationIdentityDbContext _context; // Inject your DbContext


        public RegisterModel(
    UserManager<ApplicationUser> userManager,
    IUserStore<ApplicationUser> userStore,
    SignInManager<ApplicationUser> signInManager,
    ILogger<RegisterModel> logger,
    ApplicationIdentityDbContext context,
    IEmailSender emailSender)  // Add the IEmailSender parameter
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _context = context; // Initialize the context
            _emailSender = emailSender;  // Initialize the IEmailSender
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        public SelectList ResponsibleUnits { get; set; }  // Dropdown for Responsible Units

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Designation/Role")]
            public string DesignationRole { get; set; }

            [Required]
            [Display(Name = "Unit/Office/Campus")]
            public string UnitOfficeCampus { get; set; }

            [Required]
            [Display(Name = "Responsible Unit")]  // New field for the responsible unit
            public string ResponsibleUnit { get; set; }  // Store the selected Responsible Unit

            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            // Fetch responsible units from the database
            var units = await _context.ResponsibleUnits.ToListAsync();  // Fetch the list of responsible units
            ResponsibleUnits = new SelectList(units, "Name", "Name");  // Populate the dropdown
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // Create a new user instance using the data from InputModel (except for Password)
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    DesignationRole = Input.DesignationRole,
                    UnitOfficeCampus = Input.UnitOfficeCampus,
                    PhoneNumber = Input.PhoneNumber,
                    ResponsibleUnit = Input.ResponsibleUnit
                };

                // Automatically generate a password
                var generatedPassword = GenerateRandomPassword();

                // Create the user with the generated password
                var result = await _userManager.CreateAsync(user, generatedPassword);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with autogenerated password.");

                    // Load the email template
                    string emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplate.html");
                    string emailTemplate = await System.IO.File.ReadAllTextAsync(emailTemplatePath);

                    // Generate the dynamic content
                    string messageContent = $@"
                Dear {Input.FirstName},<br><br>
                Welcome to the GEDSI Hub!<br><br>
                Your account has been successfully created. Below are your credentials:<br>
                <strong>Username:</strong> {Input.Email}<br>
                <strong>Temporary Password:</strong> {generatedPassword}<br><br>
                Please log in using these credentials and change your password at your earliest convenience.<br><br>
                Best regards,<br>
                GEDSI Hub Team
            ";

                    // Replace the placeholder in the email template with the dynamic content
                    string emailContent = emailTemplate.Replace("[MessageContent]", messageContent);

                    // Send the email with the password to the user's email
                    await _emailSender.SendEmailAsync(Input.Email, "Your GEDSI Hub Account Details", emailContent);

                    // Optionally, add the user to a default role (e.g., "User")
                    await _userManager.AddToRoleAsync(user, "User");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect("/GPBProgress");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If something went wrong, reload the responsible units to redisplay the form with errors
            var units = await _context.ResponsibleUnits.ToListAsync();
            ResponsibleUnits = new SelectList(units, "Name", "Name");

            return Page();
        }




        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
        private string GenerateRandomPassword(int length = 12)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            const string upperCaseChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            const string lowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*?_-";

            Random random = new Random();

            // Ensure the password meets the policy by including at least one of each required type
            var password = new List<char>
    {
        upperCaseChars[random.Next(upperCaseChars.Length)],
        lowerCaseChars[random.Next(lowerCaseChars.Length)],
        digits[random.Next(digits.Length)],
        specialChars[random.Next(specialChars.Length)]
    };

            // Fill the rest of the password with random characters
            password.AddRange(Enumerable.Repeat(validChars, length - 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            // Shuffle the characters to avoid predictable patterns
            return new string(password.OrderBy(x => random.Next()).ToArray());
        }


    }
}
