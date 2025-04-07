using Demo.DAL.Entities.Employees;
using Demo.DAL.Entities.Projects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entities.ProjectEmployees
{
    public class ProjectEmployee : ModelBase
    {
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        //Navigation Properties [One]
        public virtual Project Project { get; set; }
        //Navigation Properties [One]
        public virtual Employee Employee { get; set; }
        public int NumOfHours { get; set; }
    }
}
