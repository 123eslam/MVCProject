﻿using Demo.BLL.Common.Services.GetUsereLogin;
using Demo.BLL.Dtos.Projects;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Entities.Identity;
using Demo.DAL.Entities.Projects;
using Demo.DAL.Presistance.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetuserLogin _getuserLogin;

        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectService(IUnitOfWork unitOfWork,IGetuserLogin getuserLogin/*, UserManager<ApplicationUser> userManager , IHttpContextAccessor httpContextAccessor*/) 
        {
            _unitOfWork = unitOfWork;
            _getuserLogin = getuserLogin;
            //_userManager = userManager;
            //_httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> CreateProjectAsync(CreateProjectDto entity)
        {
            //var userLogin = _httpContextAccessor.HttpContext?.User;
            //var user = await _userManager.GetUserAsync(userLogin);
            _unitOfWork.ProjectRepository.Add(new Project
            {
                Name = entity.Name,
                Location = entity.Location,
                City = entity.City,
                DepartmentId = entity.DepartmentId,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = await _getuserLogin.GetUserNameLoginAsync(), //user.Id is string
                LastModifiedBy = await _getuserLogin.GetUserNameLoginAsync(),
                LastModifiedOn = DateTime.UtcNow
            });
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<int> UpdateProjectAsync(UpdateProjectDto entity) 
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(entity.Id);
            if (project is null)
                return 0;
            project.Name = entity.Name;
            project.Location = entity.Location;
            project.City = entity.City;
            project.DepartmentId = entity.DepartmentId;
            project.LastModifiedBy = await _getuserLogin.GetUserNameLoginAsync(); //user.Id is string
            project.LastModifiedOn = DateTime.UtcNow;
            _unitOfWork.ProjectRepository.Update(project);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> DeleteProjectAsync(int id)
        {
            var projectRepository = _unitOfWork.ProjectRepository;
            var project = await projectRepository.GetByIdAsync(id);
            if (project is { })
                projectRepository.Delete(project);
            return await _unitOfWork.CompleteAsync() > 0;
        }
        public async Task<ProjectDetailsDto?> GetProjectByIdAsync(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if(project is null)
                return null;
            return new ProjectDetailsDto
            {
                Id = project.Id,
                Name = project.Name,
                Location = project.Location,
                City = project.City,
                CreatedBy = project.CreatedBy,
                CreatedOn = project.CreatedOn,
                LastModifiedBy = project.LastModifiedBy,
                LastModifiedOn = project.LastModifiedOn,
                DepartmentId = project.DepartmentId,
                Department = project.Department?.Name
            };
        }
        public async Task<IEnumerable<ProjectDto>> GetProjectsAsync(string SearchValue)
        {
            var query = await _unitOfWork.ProjectRepository.GetAllQueryable()
                .Include(P => P.Department)
                .Where(P => (string.IsNullOrEmpty(SearchValue) || P.Name.ToLower().Contains(SearchValue.ToLower())))
                .Select(project => new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Location = project.Location,
                    City = project.City,
                    Department = project.Department!.Name
                }).ToListAsync();
            return query;
        }
    }
}
