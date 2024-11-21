using Elevate.BL;
using Elevate.BL.Models;
using Elevate.UI.Extensions;
using Elevate.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;

        private int GetUserIdFromContext()
        {
            var userIdSession = HttpContext.Session.GetInt32("UserId");
            if (userIdSession.HasValue)
            {
                return userIdSession.Value;
            }

            throw new Exception("User is not authenticated or UserId is missing.");
        }

        private void ClearShoppingCart()
        {
            HttpContext.Session.Remove("ShoppingCart");
        }

        // GET: ShoppingCartController
        public IActionResult Index()
        {
            ViewBag.Title = "Shopping Cart";
            cart = GetShoppingCart();
            return View(cart);
        }

        private ShoppingCart GetShoppingCart()
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") != null)
            {
                return HttpContext.Session.GetObject<ShoppingCart>("cart");
            }
            else
            {
                return new ShoppingCart();
            }
        }

        public IActionResult Remove(int id)
        {
            cart = GetShoppingCart();
            Course course = cart.Items.FirstOrDefault(i => i.Id == id);
            if (course != null)
            {
                ShoppingCartManager.Remove(cart, course);
                HttpContext.Session.SetObject("cart", cart);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add(int id)
        {
            cart = GetShoppingCart();
            if (cart.Items.Any(item => item.Id == id))
            {
                return Json(new { success = false, message = "Item is already in the cart." });
            }

            Course course = CourseManager.LoadById(id);
            if (course != null)
            {
                ShoppingCartManager.Add(cart, course);
                HttpContext.Session.SetObject("cart", cart);
            }
            return Json(new { success = true });
        }

        public IActionResult Checkout()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                int userId = GetUserIdFromContext();
                var cart = GetShoppingCart();

                ShoppingCartManager.Checkout(cart, userId);
                ClearShoppingCart();

                // Redirect to a success view
                return View("~/Views/ShoppingCart/Success.cshtml");
            }
            else
            {
                ViewBag.ShowLoginModal = true;
                return View("Index", GetShoppingCart());
            }
        }







    }
}
