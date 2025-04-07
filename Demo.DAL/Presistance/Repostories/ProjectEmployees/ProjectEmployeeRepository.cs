using Demo.DAL.Entities.ProjectEmployees;
using Demo.DAL.Presistance.Data.Context;
using Demo.DAL.Presistance.Repostories._Generic;

namespace Demo.DAL.Presistance.Repostories.ProjectEmployees
{
    public class ProjectEmployeeRepository : GenericRepository<ProjectEmployee>, IProjectEmployeeRepository
    {
        public ProjectEmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
