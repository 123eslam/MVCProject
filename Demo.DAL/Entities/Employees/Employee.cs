using Demo.DAL.Common.Enums;
using Demo.DAL.Entities.Departments;

namespace Demo.DAL.Entities.Employees
{
    public class Employee : ModelBase
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        //Navigation Properties [One]
        public virtual Department? Department { get; set; }
        //Foreign Key
        public int? DepartmentId { get; set; }
        public string? Image { get; set; } //ImageName
    }
}
