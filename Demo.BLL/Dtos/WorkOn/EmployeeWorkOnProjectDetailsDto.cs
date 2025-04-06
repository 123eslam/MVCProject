using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.WorkOn
{
    public class EmployeeWorkOnProjectDetailsDto
    {
        public int Id { get; set; }
        public string Employee { get; set; }
        public int EmployeeId { get; set; }
        public string Project { get; set; }
        public int ProjectId { get; set; }
        [Display(Name = "Duration")]
        public int NumOfHours { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = string.Empty;
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; } = string.Empty;
        [Display(Name = "Last Modified On")]
        public DateTime LastModifiedOn { get; set; }
    }
}
