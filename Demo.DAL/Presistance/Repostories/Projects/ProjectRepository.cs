using Demo.DAL.Entities.Projects;
using Demo.DAL.Presistance.Data.Context;
using Demo.DAL.Presistance.Repostories._Generic;

namespace Demo.DAL.Presistance.Repostories.Projects
{
    public class ProjectRepository : GenericRepository<Project> , IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
