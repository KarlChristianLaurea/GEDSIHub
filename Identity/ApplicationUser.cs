using Microsoft.AspNetCore.Identity;

namespace GADApplication.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DesignationRole { get; set; } 
        public string UnitOfficeCampus  { get; set; }   
        public string PhoneNumber { get; set; } 
        public string ResponsibleUnit { get; set; }
        public string? LastMessagePreview { get; set; } // Add this line

    }
}
