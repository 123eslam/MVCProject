using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Repostories._Generic;

namespace Demo.DAL.Presistance.Repostories.Departmemts
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Department? GetByName(string name);
    }
}
