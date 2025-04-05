using Demo.DAL.Presistance.Repostories.Departmemts;
using Demo.DAL.Presistance.Repostories.Employees;
using Demo.DAL.Presistance.Repostories.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        Task<int> CompleteAsync();
    }
}
