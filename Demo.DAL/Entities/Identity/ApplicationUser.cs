using Microsoft.AspNetCore.Identity;

namespace Demo.DAL.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public bool IsAgree { get; set; }
    }
}
