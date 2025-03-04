using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Data.Context;
using Demo.DAL.Presistance.Repostories._Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Presistance.Repostories.Departmemts
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Department? GetByName(string name)
        {
            return _dbContext.Departments.AsNoTracking().FirstOrDefault(D => D.Name == name);
        }
    }
}
