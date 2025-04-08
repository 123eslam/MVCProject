using AutoMapper;
using Demo.BLL.Dtos.Projects;
using Demo.BLL.Services.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectService projectService, IMapper mapper, ILogger<ProjectController> logger, IWebHostEnvironment environment)
        {
            _projectService = projectService;
            _mapper = mapper;
            _environment = environment;
            _logger = logger;
        }

        #region Index
        // GET: Project
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var projects = await _projectService.GetProjectsAsync(SearchValue);
            return View(projects);
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var project = await _projectService.GetProjectByIdAsync(id.Value);
            if (project == null)
                return NotFound();
            return View(project);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UpdateProjectDto projectDto)
        {
            if (!ModelState.IsValid)
                return View(projectDto);
            var message = string.Empty;
            try
            {
                var projectCreate = _mapper.Map<CreateProjectDto>(projectDto);
                var Result = await _projectService.CreateProjectAsync(projectCreate);
                if (Result > 0)
                {
                    TempData["Message"] = "Project created successfully";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    message = "Failed to create project";
                    TempData["Message"] = message;
                    TempData["MessageType"] = "error";
                    ModelState.AddModelError(string.Empty, message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(projectDto);
                }
                else
                {
                    message = "Failed to create project";
                    TempData["Message"] = message;
                    TempData["MessageType"] = "error";
                    return View("Error", message);
                }
            }
        }

        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var project = await _projectService.GetProjectByIdAsync(id.Value);
            if (project is null)
                return NotFound();
            var projectUpdate = _mapper.Map<UpdateProjectDto>(project);
            return View(projectUpdate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateProjectDto projectDto)
        {
            if (!ModelState.IsValid)
                return View(projectDto);
            var message = string.Empty;
            try
            {
                var result = await _projectService.UpdateProjectAsync(projectDto);
                if (result > 0)
                {
                    TempData["Message"] = "Project update successfully";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    message = "Failed to update project";
                    TempData["Message"] = message;
                    TempData["MessageType"] = "error";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "Failed to update project";
                TempData["Message"] = message;
                TempData["MessageType"] = "error";
            }
            return View(projectDto);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var project = await _projectService.GetProjectByIdAsync(id.Value);
            if (project is null)
                return NotFound();
            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var result = await _projectService.DeleteProjectAsync(id);
                if (result)
                {
                    TempData["Message"] = "Project deleted successfully";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    message = "Failed to delete project";
                    TempData["Message"] = message;
                    TempData["MessageType"] = "error";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Failed to delete project";
                TempData["Message"] = message;
                TempData["MessageType"] = "error";
            }
            return View(nameof(Index));
        }
        #endregion
    }
}
