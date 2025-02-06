using GADApplication.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

public class UserProfileModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserProfileModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public string FullName { get; set; }
    public string DesignationRole { get; set; }
    public string UnitOfficeCampus { get; set; }
    public string ResponsibleUnit { get; set; }

    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }

    public async Task OnGetAsync()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            UserName = user.UserName;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            PhoneNumber = user.PhoneNumber;
            PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            FullName = $"{user.FirstName} {user.LastName}";
            DesignationRole = user.DesignationRole;
            UnitOfficeCampus = user.UnitOfficeCampus;
            ResponsibleUnit = user.ResponsibleUnit;

            LockoutEnabled = user.LockoutEnabled;
            AccessFailedCount = user.AccessFailedCount;
        }
    }
}