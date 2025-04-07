using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.WorkOn
{
    public class UpdateEmployeeWorkOnProjectDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee is required")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Project is required")]
        public int ProjectId { get; set; }
        [Display(Name = "Duration")]
        public int NumOfHours { get; set; }
    }
}
