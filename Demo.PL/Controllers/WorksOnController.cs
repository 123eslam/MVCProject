using AutoMapper;
using Demo.BLL.Dtos.WorkOn;
using Demo.BLL.Services.WorkOn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WorksOnController : Controller
    {
        private readonly IWorkOnService _workOnService;
        private readonly IMapper _mapper;
        private readonly ILogger<WorksOnController> _logger;
        private readonly IWebHostEnvironment _environment;

        public WorksOnController(IWorkOnService workOnService, IMapper mapper, ILogger<WorksOnController> logger, IWebHostEnvironment environment)
        {
            _workOnService = workOnService;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var workOn = await _workOnService.GetWorksOnAsync(SearchValue);
            return View(workOn);
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
        public async Task<IActionResult> Create(UpdateEmployeeWorkOnProjectDto workOnProjectDto)
        {
            if (!ModelState.IsValid)
                return View(workOnProjectDto);
            var message = string.Empty;
            try
            {
                var workOnProjectCreate = _mapper.Map<AssignEmployeeWorkOnProjectDto>(workOnProjectDto);
                var Result = await _workOnService.AssignEmployeeToWorkOnProjectAsync(workOnProjectCreate);
                if (Result > 0)
                {
                    TempData["Message"] = "Employee assign to work on project successfully";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    message = "Failed to assign Employee to work on project, The Employee and the project may not belong to the same department.";
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
                    return View(workOnProjectDto);
                }
                else
                {
                    message = "Failed to assign Employee to work on project, The Employee and the project may not belong to the same department.";
                    TempData["Message"] = message;
                    TempData["MessageType"] = "error";
                    return View("Error", message);
                }
            }
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var workOnProject = await _workOnService.GetWorksOnByIdAsync(id.Value);
            if (workOnProject == null)
                return NotFound();
            return View(workOnProject);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var workOnProject = await _workOnService.GetWorksOnByIdAsync(id.Value);
            if (workOnProject is null)
                return NotFound();
            var workOnProjectUpdate = _mapper.Map<UpdateEmployeeWorkOnProjectDto>(workOnProject);
            return View(workOnProjectUpdate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateEmployeeWorkOnProjectDto workOnProjectDto)
        {
            if (!ModelState.IsValid)
                return View(workOnProjectDto);
            var message = string.Empty;
            try
            {
                var result = await _workOnService.UpdateEmployeeToWorkOnProjectAsync(workOnProjectDto);
                if (result > 0)
                {
                    TempData["Message"] = "Employee update to work on project successfully";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    message = "Failed to update employee to work on project";
                    TempData["Message"] = message;
                    TempData["MessageType"] = "error";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "Failed to update employee to work on project";
                TempData["Message"] = message;
                TempData["MessageType"] = "error";
            }
            return View(workOnProjectDto);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var workOnProject = await _workOnService.GetWorksOnByIdAsync(id.Value);
            if (workOnProject == null)
                return NotFound();
            return View(workOnProject);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var result = await _workOnService.DeleteEmployeeToWorkOnProjectAsync(id);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Failed to delete employee to work on project";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Failed to delete employee to work on project";
            }
            return View(nameof(Index));
        }
        #endregion
    }
}
