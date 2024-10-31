using Elevate.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.BL.Test
{
    [TestClass]
    public class ShoppingCartManagerTest
    {
        [TestMethod]
        public void AddTest()
        {
            ShoppingCart cart = new ShoppingCart();
            List<Course> list = CourseManager.Load();
            ShoppingCartManager.Add(cart, list[0]);
            ShoppingCartManager.Add(cart, list[1]);
            Assert.AreEqual(2, cart.TotalCount);
            Assert.AreEqual(28.98, cart.SubTotal);
            Assert.AreEqual(1.5939, cart.Tax);
            Assert.AreEqual(30.57, Math.Round(cart.Total, 2));
        }
    }
}
