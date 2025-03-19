using Demo.BLL.Dtos.Employees;

namespace Demo.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string SearchValue);
        Task<EmployeeDetailesDto?> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(CreateEmployeeDto entity);
        Task<int> UpdateEmployeeAsync(UpdateEmployeeDto entity);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
