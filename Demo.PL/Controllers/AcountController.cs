using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AcountController : Controller
    {
        //Register , Login , Signout
        [HttpGet] //Display Register Form
        public IActionResult Register()
        {
            return View();
        }
    }
}
