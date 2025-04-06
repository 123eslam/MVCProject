using Demo.DAL.Entities.Departments;
using Demo.DAL.Entities.ProjectEmployees;

namespace Demo.DAL.Entities.Projects
{
    public class Project : ModelBase
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        //Navigation Properties [One]
        public virtual Department? Department { get; set; }
        //Foreign Key
        public int? DepartmentId { get; set; }
        //Navigation Properties [Many]
        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new HashSet<ProjectEmployee>();
    }
}
