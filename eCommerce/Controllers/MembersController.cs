using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Check DB for credentials 
                Member? m = (from member in _context.Members
                            where member.Email == loginModel.Email
                            && member.Password == loginModel.Password
                            select member).SingleOrDefault();
                // If exists, send to homepage
                if (m != null)
                {
                    HttpContext.Session.SetString("Email", loginModel.Email);
                    return RedirectToAction("Index", "Home");
                }
                // If no record matches, display error
                ModelState.AddModelError(String.Empty, "Credentials not found!");
            }
            // Return page if no record is found, or ModelState is invalid
            return View(loginModel);
        }
    }
}
