﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Identity
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;
    }
}
