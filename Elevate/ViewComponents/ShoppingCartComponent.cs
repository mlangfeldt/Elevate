using Elevate.BL.Models;
using Elevate.UI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.UI.ViewComponents
{
    public class ShoppingCartComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            // Retrieve the ShoppingCart from the session, or create a new one if it doesn't exist
            var cart = _httpContextAccessor.HttpContext.Session.GetObject<ShoppingCart>("cart") ?? new ShoppingCart();

            // Pass the cart to the view
            return View(cart);
        }
    }
}
