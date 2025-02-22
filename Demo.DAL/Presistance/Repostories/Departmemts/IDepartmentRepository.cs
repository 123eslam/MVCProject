using Demo.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repostories.Departmemts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool AsNoTracking = true);
        Department? GetByID(int id);
        int AddDepartment(Department entity);
        int UpdateDepartment(Department entity);
        int DeleteDepartment(Department entity);
    }
}
