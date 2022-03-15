using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly VideoGameContext _context;
        private const string Cart = "shoppingCart";
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

            WriteShoppingCartCookie(cartGames);
            TempData["Message"] = "Item added to cart";
            return RedirectToAction("Index", "Games");
        }

        private void WriteShoppingCartCookie(List<CartGameViewModel> cartGames)
        {
            string cookieData = JsonConvert.SerializeObject(cartGames);

            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });
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
        public IActionResult Summary()
        {
            // read shopping cart data and convert to list of the view model 
            List<CartGameViewModel> cartGames = GetExistingCartData();
            return View(cartGames);
        }

        public IActionResult Remove(int id)
        {
            List <CartGameViewModel> cartGames = GetExistingCartData();

            CartGameViewModel targetGame = cartGames.FirstOrDefault(g => g.GameId == id);

            cartGames.Remove(targetGame);
            WriteShoppingCartCookie(cartGames);

            return RedirectToAction(nameof(Remove));
        }
    }
}
