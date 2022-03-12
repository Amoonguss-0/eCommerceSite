using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
