using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.BL.Test
{
    [TestClass]
    public class utOrderItem
    {
        [TestMethod]
        public void InsertTest()
        {
            OrderItem orderItem = new OrderItem
            {
                OrderId = 4,
                CourseId = 4,
                Quantity = 2,
                Cost = 14.50
            };

            Assert.AreEqual(1, OrderItemManager.Insert(orderItem, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderItemManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(1, OrderItemManager.LoadById(1).Id);
        }
        [TestMethod]
        public void LoadByOrderIdTest()
        {
            int orderId = OrderItemManager.Load().FirstOrDefault().OrderId;
            Assert.IsTrue(OrderItemManager.LoadByOrderId(orderId).Count > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            OrderItem orderItem = OrderItemManager.LoadById(3);
            orderItem.Cost = 26.50;
            Assert.IsTrue(OrderItemManager.Update(orderItem, true) > 0);
        }
        [TestMethod]
        public void DeleteTest()
        {
            Assert.AreEqual(1, OrderItemManager.Delete(2, true));
        }
    }
}
