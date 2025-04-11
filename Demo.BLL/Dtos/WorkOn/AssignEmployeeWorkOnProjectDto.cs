using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.WorkOn
{
    public class AssignEmployeeWorkOnProjectDto
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        [Display(Name = "Duration")]
        public int NumOfHours { get; set; }
    }
}
