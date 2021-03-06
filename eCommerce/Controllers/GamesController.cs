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

        public async Task<IActionResult> Index(int? id)
        {
            // numGames is the amount of games shown per page
            const int numGames = 3;
            const int pageOffset = 1; // used to offset current page and only put 3 games on the page at a time
            int currPage = id ?? 1; // Set currPage to id if it has a value otherwise use 1
            // Get all games from the DB

            int totalNumOfProducts = await _context.Games.CountAsync();
            double maxNumPages = Math.Ceiling((double)totalNumOfProducts / numGames);
            int lastPage = Convert.ToInt32(maxNumPages); // rounds pages up, to the next whole page number

            List<Game> games = await (from Game in _context.Games
                                      select Game)
                                      .Skip(numGames * (currPage - pageOffset))
                                      .Take(numGames)
                                      .ToListAsync();
            // Show them on the web page
            GameCatalogViewModel catalogModel = new(games, lastPage, currPage);
            return View(catalogModel);
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Game? gameToEdit = await _context.Games.FindAsync(id);  

            if (gameToEdit == null)
            {
                return NotFound();
            }
            return View(gameToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Game gameModel)
        {
            if (ModelState.IsValid)
            {
                _context.Games.Update(gameModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{gameModel.Title} was updated successfully";
                return RedirectToAction("Index");
            }
            return View(gameModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Game gameToDelete = await _context.Games.FindAsync(id);

            if (gameToDelete == null)
            {
                return NotFound();
            }
            return View(gameToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Game gameToDelete = await _context.Games.FindAsync(id);
            if (gameToDelete != null) 
            {
                _context.Games.Remove(gameToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = gameToDelete.Title + " was deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "This game was already deleted";
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Details(int id)
        {
            var gameDetails = await _context.Games.FindAsync(id);

            if (gameDetails == null)
            {
                return NotFound();
            }
            return View(gameDetails);
        }
    }
}
