using Demo.BLL.Dtos.Departments;
using Demo.BLL.Dtos.Employees;
using Demo.BLL.Services.Departments;
using Demo.BLL.Services.Employees;
using Demo.DAL.Common.Enums;
using Demo.DAL.Entities.Departments;
using Demo.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetEmployees();
            return View(employees);
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
        public IActionResult Create(UpdateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);
            var message = string.Empty;
            try
            {
                var Result = _employeeService.CreateEmployee(new CreateEmployeeDto()
                {
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    Address = employeeDto.Address,
                    Salary = employeeDto.Salary,
                    IsActive = employeeDto.IsActive,
                    Email = employeeDto.Email,
                    PhoneNumber = employeeDto.PhoneNumber,
                    HiringDate = employeeDto.HiringDate,
                    EmployeeType = employeeDto.EmployeeType,
                    Gender = employeeDto.Gender,
                    DepartmentId = employeeDto.DepartmentId
                });
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
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();
            return View(employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();
            return View(new UpdateEmployeeDto()
            {
                Id = id.Value,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = (Gender)Enum.Parse(typeof(Gender), employee.Gender),
                EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), employee.EmployeeType)
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UpdateEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                var result = _employeeService.UpdateEmployee(employee);
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
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var result = _employeeService.DeleteEmployee(id);
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
