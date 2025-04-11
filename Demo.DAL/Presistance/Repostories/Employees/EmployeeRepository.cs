using Demo.DAL.Entities.Employees;
using Demo.DAL.Presistance.Data.Context;
using Demo.DAL.Presistance.Repostories._Generic;

namespace Demo.DAL.Presistance.Repostories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
