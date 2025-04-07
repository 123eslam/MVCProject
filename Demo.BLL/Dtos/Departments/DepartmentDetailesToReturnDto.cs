using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Dtos.Departments
{
    public class DepartmentDetailesToReturnDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public DateOnly CreationDate { get; set; }
    }
}
