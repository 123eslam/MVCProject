﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Users
{
    public class UsersViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        [EmailAddress]
        public string? Email { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
