using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly VideoGameContext _context;
        private const string Cart = "Shopping Cart";
        public CartController(VideoGameContext context)
        {
            _context = context;
        }
        public IActionResult Add(int id)
        {
            Game? gameToAdd = _context.Games.Where(g => g.GameId == id).SingleOrDefault();

            if (gameToAdd == null)
            {
                TempData["Message"] = "Sorry that game no longer exists";
                return RedirectToAction("Index", "Games");           
            }
            CartGameViewModel cartGame = new()
            {
                GameId = gameToAdd.GameId,
                Title = gameToAdd.Title,
                Price = gameToAdd.Price
            };

            List<CartGameViewModel> cartGames = GetExistingCartData();
            cartGames.Add(cartGame);

            string cookieData = JsonConvert.SerializeObject(cartGames);

            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });
            TempData["Message"] = "Item added to cart";
            return RedirectToAction("Index", "Games");
        }

        private List<CartGameViewModel> GetExistingCartData()
        {
            string cookie = HttpContext.Request.Cookies[Cart];
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return new List<CartGameViewModel>();
            }
            return JsonConvert.DeserializeObject<List<CartGameViewModel>>(cookie);
        }
    }
}
