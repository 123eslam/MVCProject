using Demo.BLL.Dtos.Departments;
using Demo.BLL.Services.Departments;
using Demo.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetDepartments();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentToCreateDto department)
        {
            if (!ModelState.IsValid)
                return View(department);
            var message = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(department);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Failed to create department";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(department);
                }
                else
                {
                    message = "Failed to create department";
                    return View("Error", message);
                }
            }
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();
            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate,
                Name = department.Name
            });
        }
        [HttpPost]
        public IActionResult Edit(int id, DepartmentEditViewModel department)
        {
            if (!ModelState.IsValid)
                return View(department);
            var message = string.Empty;
            try
            {
                var result = _departmentService.UpdateDepartment(new DepartmentToUpdateDto()
                {
                    Id = id,
                    Code = department.Code,
                    Description = department.Description,
                    Name = department.Name
                });
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
    }
}
