using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Roles;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<UserController> _logger;

        public RoleController(RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment, ILogger<UserController> logger)
        {
            _roleManager = roleManager;
            _environment = environment;
            _logger = logger;
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if (!ModelState.IsValid)
                return View(roleVM);
            var message = string.Empty;
            try
            {
                var role = new IdentityRole()
                {
                    Name = roleVM.Name
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Role creation failed";
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "Role to create user";
            }
            return View(roleVM);
        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var rolesQuery = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrWhiteSpace(SearchValue))
                rolesQuery = rolesQuery.Where(R => R.Name.ToLower().Contains(SearchValue.ToLower()));
            var rolesList = await rolesQuery.Select(R => new RoleViewModel()
            {
                Id = R.Id,
                Name = R.Name
            }).ToListAsync();
            return View(rolesList);
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(roleVM);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(roleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RoleViewModel roleVM)
        {
            if (!ModelState.IsValid)
                return View(roleVM);
            var message = string.Empty;
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return NotFound();
                role.Name = roleVM.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Role update failed";
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "Role to update user";
            }
            return View(roleVM);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(roleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var message = string.Empty;
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is not null)
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Failed to delete role";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Failed to delete role";
            }
            return View(nameof(Index));
        }
        #endregion
    }
}
