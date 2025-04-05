using Demo.BLL.Dtos.Projects;

namespace Demo.BLL.Services.Projects
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetProjectsAsync(string SearchValue);
        Task<ProjectDetailsDto?> GetProjectByIdAsync(int id);
        Task<int> CreateProjectAsync(CreateProjectDto entity);
        Task<int> UpdateProjectAsync(UpdateProjectDto entity);
        Task<bool> DeleteProjectAsync(int id);
    }
}
