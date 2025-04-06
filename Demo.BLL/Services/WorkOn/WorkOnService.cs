using Demo.BLL.Common.Services.GetUsereLogin;
using Demo.BLL.Dtos.WorkOn;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Entities.ProjectEmployees;
using Demo.DAL.Entities.Projects;
using Demo.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.WorkOn
{
    public class WorkOnService : IWorkOnService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetuserLogin _getuserLogin;

        public WorkOnService(IUnitOfWork unitOfWork, IGetuserLogin getuserLogin)
        {
            _unitOfWork = unitOfWork;
            _getuserLogin = getuserLogin;
        }
        public async Task<int> AssignEmployeeToWorkOnProjectAsync(AssignEmployeeWorkOnProjectDto entity)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(entity.ProjectId);
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(entity.EmployeeId);
            if (project is null || employee is null)
                return 0;
            if (employee.DepartmentId != project.DepartmentId)
                return 0;
            _unitOfWork.ProjectEmployeeRepository.Add(new ProjectEmployee
            {
                EmployeeId = entity.EmployeeId,
                ProjectId = entity.ProjectId,
                NumOfHours = entity.NumOfHours,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = await _getuserLogin.GetUserNameLoginAsync(),
                LastModifiedBy = await _getuserLogin.GetUserNameLoginAsync(),
                LastModifiedOn = DateTime.UtcNow
            });
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<int> UpdateEmployeeToWorkOnProjectAsync(UpdateEmployeeWorkOnProjectDto entity)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(entity.ProjectId);
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(entity.EmployeeId);
            if (project is null || employee is null)
                return 0;
            if (employee.DepartmentId != project.DepartmentId)
                return 0;
            var workOn = await _unitOfWork.ProjectEmployeeRepository.GetByIdAsync(entity.Id);
            if (workOn is null)
                return 0;
            workOn.EmployeeId = entity.EmployeeId;
            workOn.ProjectId = entity.ProjectId;
            workOn.NumOfHours = entity.NumOfHours;
            workOn.LastModifiedBy = await _getuserLogin.GetUserNameLoginAsync();
            workOn.LastModifiedOn = DateTime.UtcNow;
            _unitOfWork.ProjectEmployeeRepository.Update(workOn);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> DeleteEmployeeToWorkOnProjectAsync(int id)
        {
            var workOnRepo = _unitOfWork.ProjectEmployeeRepository;
            var workOn = await workOnRepo.GetByIdAsync(id);
            if(workOn is not null)
                workOnRepo.Delete(workOn);
            return await _unitOfWork.CompleteAsync() > 0;
        }
        public async Task<IEnumerable<EmployeeWorkOnProjectDto>> GetWorksOnAsync(string SearchValue)
        {
            var query = await _unitOfWork.ProjectEmployeeRepository.GetAllQueryable()
                .Include(W => W.Project)
                .Include(W => W.Employee)
                .Where(W => (string.IsNullOrEmpty(SearchValue) || W.Project.Name.ToLower().Contains(SearchValue.ToLower())))
                .Select(W => new EmployeeWorkOnProjectDto
                {
                    Id = W.Id,
                    Project = W.Project.Name,
                    Employee = W.Employee.Name,
                    NumOfHours = W.NumOfHours
                }).ToListAsync();
            return query;
        }
        public async Task<EmployeeWorkOnProjectDetailsDto?> GetWorksOnByIdAsync(int id)
        {
            var workOn = await _unitOfWork.ProjectEmployeeRepository.GetByIdAsync(id);
            if (workOn is not null)
                return new EmployeeWorkOnProjectDetailsDto
                {
                    Id = workOn.Id,
                    Project = workOn.Project.Name,
                    ProjectId = workOn.ProjectId,
                    Employee = workOn.Employee.Name,
                    EmployeeId = workOn.EmployeeId,
                    NumOfHours = workOn.NumOfHours,
                    CreatedBy = workOn.CreatedBy,
                    CreatedOn = workOn.CreatedOn,
                    LastModifiedBy = workOn.LastModifiedBy,
                    LastModifiedOn = workOn.LastModifiedOn
                };
            return null;
        }
    }
}
