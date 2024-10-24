using Elevate.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.BL.Test
{
    [TestClass]
    public class utOrder
    {
        [TestMethod]
        public void InsertTest()
        {
            Order order = new Order
            {
                CustomerId = 4,
                OrderDate = DateTime.Now,
                UserId = 3,
                OrderItems = new List<OrderItem>()
            };

            Assert.AreEqual(1, OrderManager.Insert(order, true));
        }
        [TestMethod]
        public void InsertOrderItemsTest()
        {
            Order order = new Order
            {
                CustomerId = 99,
                OrderDate = DateTime.Now,
                UserId = 99,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Id = 88,
                        CourseId = 1,
                        Quantity = 9,
                        Cost = 9.99
                    },
                    new OrderItem
                    {
                        Id = 99,
                        CourseId = 2,
                        Quantity = 9,
                        Cost = 8.88
                    }
                }
            };
            int result = OrderManager.Insert(order, true);
            Assert.AreEqual(order.OrderItems[1].OrderId, order.Id);
            Assert.AreEqual(3, result);
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            int id = OrderManager.Load().LastOrDefault().Id;
            Order order = OrderManager.LoadById(id);
            Assert.AreEqual(order.Id, id);
            Assert.IsTrue(order.OrderItems.Count > 0);
        }
        [TestMethod]
        public void LoadByCustomerIdTest()
        {
            int customerId = OrderManager.Load().FirstOrDefault().CustomerId;
            Assert.AreEqual(OrderManager.LoadById(customerId).CustomerId, customerId);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Order order = OrderManager.LoadById(3);
            order.UserId = 2;
            Assert.IsTrue(OrderManager.Update(order, true) > 0);
        }
        [TestMethod]
        public void DeleteTest()
        {
            Assert.AreEqual(1, OrderManager.Delete(2, true));
        }
    }
}
