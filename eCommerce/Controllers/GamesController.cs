using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class GamesController : Controller
    {
        private readonly VideoGameContext _context;
        public GamesController(VideoGameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            if (ModelState.IsValid)
            {
                //

                _context.Games.Add(game); // Prepares insert
                await _context.SaveChangesAsync(); // Executes pending inset

                ViewData["Message"] = $"{game.Title} was added successfully!"; // Show a success message on the page           
                return View();
            }
            return View(game);
        }
    }
}
