using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Identity
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
