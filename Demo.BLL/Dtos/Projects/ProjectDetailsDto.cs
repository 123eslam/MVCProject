using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.Projects
{
    public class ProjectDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
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
    }
}
