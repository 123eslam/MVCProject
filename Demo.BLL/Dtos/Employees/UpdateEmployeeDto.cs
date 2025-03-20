using Demo.DAL.Common.Enums;
using Demo.DAL.Entities.Departments;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Dtos.Employees
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [MinLength(5, ErrorMessage = "Name cannot be shorter than 5 characters.")]
        public string Name { get; set; } = null!;

        [Range(22, 30, ErrorMessage = "Age must be between 22 and 30.")]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be in the format of 123-Street-City-Country ")]
        public string? Address { get; set; }

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }
        [Display(Name = "Employee Type")]
        public EmployeeType EmployeeType { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
