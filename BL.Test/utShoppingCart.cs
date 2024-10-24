using BL.Models;
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
            Assert.AreEqual(100 * cart.TotalCount, cart.SubTotal);
            Assert.AreEqual(100 * cart.TotalCount * .055, cart.Tax);
            Assert.AreEqual(100 * cart.TotalCount + 100 * cart.TotalCount * .055, cart.Total);
        }
    }
}
