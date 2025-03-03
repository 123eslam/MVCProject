using Demo.BLL.Dtos.Employees;

namespace Demo.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees();
        EmployeeDetailesDto? GetEmployeeById(int id);
        int CreateEmployee(CreateEmployeeDto entity);
        int UpdateEmployee(UpdateEmployeeDto entity);
        bool DeleteEmployee(int id);
    }
}
