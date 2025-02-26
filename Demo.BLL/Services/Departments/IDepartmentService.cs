using Demo.BLL.Dtos.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentToReturnDto> GetDepartments();
        DepartmentDetailesToReturnDto? GetDepartmentById(int id);
        int CreateDepartment(DepartmentToCreateDto entity);
        int UpdateDepartment(DepartmentToUpdateDto entity);
        bool DeleteDepartment(int id);
    }
}
