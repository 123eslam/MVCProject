using Demo.DAL.Entities.Departments;

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
    }
}
