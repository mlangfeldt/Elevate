using Elevate.BL.Models;
using Elevate.BL;
using Elevate.UI.Extensions;
using Elevate.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Elevate.UI.ViewModels;

namespace Elevate.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;

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
            ShoppingCartManager.Remove(cart, course);
            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add(int id)
        {
            cart = GetShoppingCart();
            Course course = CourseManager.LoadById(id);
            ShoppingCartManager.Add(cart, course);
            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction(nameof(Index), "Course");
        }

        public IActionResult Checkout()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                cart = GetShoppingCart();
                ShoppingCartManager.Checkout(cart);
                //HttpContext.Session.SetObject("cart", null);
                return RedirectToAction("AssignToCustomer");
            }

            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

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

                ShoppingCartManager.Checkout(customerVM.Cart);

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
