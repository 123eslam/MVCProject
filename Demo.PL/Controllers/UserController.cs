using Demo.BLL.Dtos.Employees;
using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager,IWebHostEnvironment environment, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _environment = environment;
            _logger = logger;
        }
        
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var usersQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(SearchValue))
                usersQuery = usersQuery.Where(U => U.Email.ToLower().Contains(SearchValue.ToLower()));
            var usersList = await usersQuery.Select(U => new UsersViewModel()
            {
                Id = U.Id,
                FName = U.FName,
                LName = U.LName,
                Email = U.Email
            }).ToListAsync();
            foreach (var user in usersList)
                user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
            return View(usersList);
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var userVM = new UsersViewModel()
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return View(userVM);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var userVM = new UsersViewModel()
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return View(userVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UsersViewModel usersVM)
        {
            if (!ModelState.IsValid)
                return View(usersVM);
            var message = string.Empty;
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                    return NotFound();
                user.FName = usersVM.FName;
                user.LName = usersVM.LName;
                user.Email = usersVM.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "User update failed";
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "Failed to update user";
            }
            return View(usersVM);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var userVM = new UsersViewModel()
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return View(userVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var message = string.Empty;
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is not null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Failed to delete user";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Failed to delete user";
            }
            return View(nameof(Index));
        }
        #endregion
    }
}
