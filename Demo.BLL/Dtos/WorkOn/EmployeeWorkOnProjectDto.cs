using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.WorkOn
{
    public class EmployeeWorkOnProjectDto
    {
        public int Id { get; set; }
        public string Employee {  get; set; }
        public string Project {  get; set; }
        [Display(Name = "Duration")]
        public int NumOfHours { get; set; }
    }
}
