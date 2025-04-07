using Demo.DAL.Entities.Employees;
using Demo.DAL.Entities.Projects;

namespace Demo.DAL.Entities.Departments
{
    public class Department : ModelBase
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public DateOnly CreationDate { get; set; }
        //Navigation Properties [Many]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        //Navigation Properties [Many]
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
