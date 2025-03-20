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
        Task<IEnumerable<DepartmentToReturnDto>> GetDepartmentsAsync();
        Task<DepartmentDetailesToReturnDto?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync(DepartmentToCreateDto entity);
        Task<int> UpdateDepartmentAsync(DepartmentToUpdateDto entity);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
