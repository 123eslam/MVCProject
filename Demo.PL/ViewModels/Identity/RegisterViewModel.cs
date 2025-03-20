using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last Name is Required")]
        public string LName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public bool IsAgree { get; set; } 
    }
}
