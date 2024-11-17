using Microsoft.AspNetCore.Mvc;

namespace EmployeeNexus.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Logout()
        {
            return View();
        }
    }
}
