using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AcountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AcountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        //Register , Login , Signout
        [HttpGet] //Display Register Form
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    //eslam123@gmail.com --> eslam123
                    UserName = registerVM.Email.Split('@')[0],
                    Email = registerVM.Email,
                    FName = registerVM.FName,
                    LName = registerVM.LName,
                    IsAgree = registerVM.IsAgree
                };
                var Result = await _userManager.CreateAsync(User, registerVM.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var Error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, Error.Description);
                    }
                }
            }
            return View(registerVM);
        }
    }
}
