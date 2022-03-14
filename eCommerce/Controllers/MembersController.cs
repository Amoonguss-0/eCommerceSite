using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class MembersController : Controller
    {
        private readonly VideoGameContext _context;

        public MembersController(VideoGameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // map registerViewModel data to member object
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();
                // Add user to database
                // redirect to home page
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }
    }
}
