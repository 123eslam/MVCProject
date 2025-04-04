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

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
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
    }
}
