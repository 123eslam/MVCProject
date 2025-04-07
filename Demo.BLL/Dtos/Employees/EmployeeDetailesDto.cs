using Demo.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Dtos.Employees
{
    public class EmployeeDetailesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public string Gender { get; set; } = null!;
        [Display(Name = "Employee Type")]
        public string EmployeeType { get; set; } = null!;
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = string.Empty;
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; } = string.Empty;
        [Display(Name = "Last Modified On")]
        public DateTime LastModifiedOn { get; set; }
        public string? Department { get; set; }
        public int? DepartmentId { get; set; }
        public string? Image { get; set; }
    }
}
