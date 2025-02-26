﻿using Demo.BLL.Dtos.Departments;
using Demo.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,ILogger<DepartmentController> logger,IWebHostEnvironment environment)
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
            catch(Exception ex)
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
    }
}
