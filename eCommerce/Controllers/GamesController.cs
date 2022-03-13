using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    public class GamesController : Controller
    {
        private readonly VideoGameContext _context;
        public GamesController(VideoGameContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get all games from the DB
            List<Game> games = await (from Game in _context.Games
                                      select Game).ToListAsync();
            // Show them on the web page

            return View(games);
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
                _context.Games.Add(game); // Prepares insert
                await _context.SaveChangesAsync(); // Executes pending inset

                ViewData["Message"] = $"{game.Title} was added successfully!"; // Show a success message on the page           
                return View();
            }
            return View(game);
        }
    }
}
