using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
