using Demo.DAL.Entities.Employees;
using Demo.DAL.Presistance.Repostories._Generic;

namespace Demo.DAL.Presistance.Repostories.Employees
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }
}
