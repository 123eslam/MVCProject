using Demo.BLL.Dtos.Employees;
using Demo.BLL.Dtos.WorkOn;

namespace Demo.BLL.Services.WorkOn
{
    public interface IWorkOnService
    {
        Task<IEnumerable<EmployeeWorkOnProjectDto>> GetWorksOnAsync(string SearchValue);
        Task<EmployeeWorkOnProjectDetailsDto?> GetWorksOnByIdAsync(int id);
        Task<int> AssignEmployeeToWorkOnProjectAsync(AssignEmployeeWorkOnProjectDto entity);
        Task<int> UpdateEmployeeToWorkOnProjectAsync(UpdateEmployeeWorkOnProjectDto entity);
        Task<bool> DeleteEmployeeToWorkOnProjectAsync(int id);
    }
}
