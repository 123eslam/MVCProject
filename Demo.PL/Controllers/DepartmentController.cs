using AutoMapper;
using Demo.BLL.Dtos.Departments;
using Demo.BLL.Services.Departments;
using Demo.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]//Only Authenticated Users
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Message"] = "Hello from View Data";
            ViewBag.Message = "Hello from View Bag";
            var departments = await _departmentService.GetDepartmentsAsync();
            return View(departments);
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
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);
            var message = string.Empty;
            try
            {
                var departmentToCreate = _mapper.Map<DepartmentViewModel, DepartmentToCreateDto>(departmentVM);
                var result = await _departmentService.CreateDepartmentAsync(departmentToCreate);
                if (result > 0)
                {
                    TempData["Message"] = "Department created successfully";
                }
                else
                {
                    message = "Failed to create department";
                    TempData["Message"] = message;
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
                    return View(departmentVM);
                }
                else
                {
                    message = "Failed to create department";
                    return View("Error", message);
                }
            }
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            var departmentVM = _mapper.Map<DepartmentDetailesToReturnDto, DepartmentViewModel>(department);
            return View(departmentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentViewModel department)
        {
            if (!ModelState.IsValid)
                return View(department);
            var message = string.Empty;
            try
            {
                var departmentToUpdate = _mapper.Map<DepartmentViewModel, DepartmentToUpdateDto>(department);
                departmentToUpdate.Id = id;
                var result = await _departmentService.UpdateDepartmentAsync(departmentToUpdate);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Failed to update department";
                }
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "Failed to update department";
            }
            return View(department);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var result = await _departmentService.DeleteDepartmentAsync(id);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Failed to delete department";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Failed to delete department";
            }
            return View(nameof(Index));
        } 
        #endregion
    }
}
