using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AcountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> _signInManager;

        public AcountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
        [HttpGet] //Display Login Form
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email); //Check if the email is exist
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, loginVM.Password); //Check if the password is correct
                    if (flag) // email exist and password correct
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                        else
                            ModelState.AddModelError(string.Empty, "Can not login");
                    }
                    else // email exist but password is incorrect
                        ModelState.AddModelError(string.Empty, "Password is incorrect");
                }
                else // email not exist
                    ModelState.AddModelError(string.Empty, "Email is not found");
            }
            return View(loginVM);
        }
        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
