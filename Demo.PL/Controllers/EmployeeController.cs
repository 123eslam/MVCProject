using AutoMapper;
using Demo.BLL.Dtos.Employees;
using Demo.BLL.Services.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]//Only Authenticated Users
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var employees = await _employeeService.GetEmployeesAsync(SearchValue);
            return View(employees);
        }
        public async Task<IActionResult> Search(string SearchValue)
        {
            var employees = await _employeeService.GetEmployeesAsync(SearchValue);
            return View("Partials/EmployeeTablePartial",employees);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create(/*[FromServices] IDepartmentService departmentService*/)
        {
            //ViewData["Departments"] = departmentService.GetDepartments();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UpdateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);
            var message = string.Empty;
            try
            {
                var employeeCreate = _mapper.Map<CreateEmployeeDto>(employeeDto);
                var Result = await _employeeService.CreateEmployeeAsync(employeeCreate);
                if (Result > 0)
                {
                    TempData["Message"] = "Employee created successfully";
                }
                else
                {
                    message = "Failed to create Employee";
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
                    return View(employeeDto);
                }
                else
                {
                    message = "Failed to create Employee";
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
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
                return NotFound();
            return View(employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            var employeeUpdate = _mapper.Map<UpdateEmployeeDto>(employee);
            return View(employeeUpdate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(employee);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Failed to update employee";
                }
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "Failed to update employee";
            }
            return View(employee);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Failed to delete employee";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Failed to delete employee";
            }
            return View(nameof(Index));
        } 
        #endregion
    }
}
