﻿using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.Projects
{
    public class UpdateProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
    }
}
