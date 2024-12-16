using Elevate.BL;
using Elevate.BL.Models;
using Elevate.UI.Extensions;
using Elevate.UI.Models;
using Elevate.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;

        User user;

        // GET: ShoppingCartController
        public IActionResult Index()
        {
            ViewBag.Title = "Shopping Cart";
            cart = GetShoppingCart();
            return View(cart);
        }

        private void ClearShoppingCart()
        {
            HttpContext.Session.Remove("cart");
        }

        private User GetUser()
        {
            if (HttpContext.Session.GetObject<User>("user") != null)
            {
                return HttpContext.Session.GetObject<User>("user");
            }
            else
            {
                return new User();
            }
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
            ShoppingCartManager.Remove(cart, course);
            HttpContext.Session.SetObject("cart", cart);
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
            ShoppingCartManager.Add(cart, course);
            HttpContext.Session.SetObject("cart", cart);

            return Json(new { success = true });
        }

        public IActionResult Checkout()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                var cart = GetShoppingCart();
                var user = GetUser();

                ShoppingCartManager.Checkout(cart, user);
                ClearShoppingCart();

                return View();
            }
            else
            {
                ViewBag.ShowLoginModal = true;
                return View("Index", GetShoppingCart());
            }
        }


        public IActionResult AssignToCustomer()
        {
            // Check to see if the user is logged in
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                // Create a CustomerViewModel and customer
                CustomerViewModel customerVM = new CustomerViewModel();
                Customer customer = new Customer();
                customerVM.Customers = CustomerManager.Load();
                customerVM.UserId = customer.UserId;

                customerVM.Cart = GetShoppingCart();

                customerVM.CustomerId = customer.Id;

                HttpContext.Session.SetObject("customerVM", customerVM);

                ViewData["ReturnUrl"] = UriHelper.GetDisplayUrl(HttpContext.Request);

                return View(customerVM);
            }

            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        [HttpPost]
        public IActionResult AssignToCustomer(CustomerViewModel customerVM)
        {
            try
            {

                customerVM.Cart = GetShoppingCart();

                ShoppingCartManager.Checkout(customerVM.Cart, user);

                HttpContext.Session.SetObject("cart", null);

                return View("Checkout");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


