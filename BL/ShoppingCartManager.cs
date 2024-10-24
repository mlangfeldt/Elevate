using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.BL
{
    public class ShoppingCartManager
    {
        public static void Add(ShoppingCart cart, Course course)
        {
            cart.Items.Add(course);
        }
        public static void Remove(ShoppingCart cart, Course course)
        {
            cart.Items.Remove(course);
        }
        public static void Checkout(ShoppingCart cart)
        {
            try
            {
                Order order = new Order
                {
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    UserId = 1,
                    OrderItems = new List<OrderItem>()
                };
                foreach (Course item in cart.Items)
                {
                    OrderItem orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        CourseId = item.Id,
                        Quantity = 1,
                        Cost = item.Cost
                    };
                    order.OrderItems.Add(orderItem);
                }
                OrderManager.Insert(order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
