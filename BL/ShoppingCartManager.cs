using Elevate.BL.Models;
using Microsoft.AspNetCore.Razor.Language.Extensions;
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
        public static void Checkout(ShoppingCart cart, User user)
        {
            try
            {
                
                User ReceiptUser = user;

                Customer ReceiptCustomer = new Customer();

                // search for the related Customer record in db based on the passed User's id
                bool FoundCustomer = CustomerManager.FindByUserId(user.Id);

                // if found load into ReceiptCustomer
                if (FoundCustomer) 
                { 
                    ReceiptCustomer = CustomerManager.LoadByUserId(user.Id); 
                }
                // otherwise insert new Customer into db
                else
                {
                    ReceiptCustomer.FirstName = ReceiptUser.FirstName;
                    ReceiptCustomer.LastName = ReceiptUser.LastName;   
                    ReceiptCustomer.Email = ReceiptUser.Email;
                    ReceiptCustomer.UserId = ReceiptUser.Id;

                    CustomerManager.Insert(ReceiptCustomer);
                    
                }

                Order ReceiptOrder = new Order
                {
                    
                    CustomerId = ReceiptCustomer.Id,
                    OrderDate = DateTime.Now,
                    UserId = ReceiptCustomer.UserId,
                    OrderItems = new List<OrderItem>()

                };

                string ReceiptBody = 
                    
                ReceiptBody = "<div id=\"header\" style=\"font-style: serif; font-size: 18px; width: 800px;\">";
                
                ReceiptBody += "<div id=\"centered\" style = \"margin: 0 auto; width:100%;\">";

                // header
                ReceiptBody += "<div style=\"width: 20%; float:left;\">" + "Course Name" + "</div>";
                ReceiptBody += "<div style=\"width: 50%; float:left;\">" + "Description" + "</div>";
                ReceiptBody += "<div style=\"width: 10%; float:left;\">" + "Quantity" + "</div>";
                ReceiptBody += "<div style=\"width: 20%; float:left;\">" + "Cost" + "</div>";

                // horizontal rule
                ReceiptBody += "<div style=\"width: 100%;\"><hr style='background-color:#000000;border-width:0;color:#000000;height:1px;line-height:0;text-align:left;width:100%;'/></div>";

                foreach (Course item in cart.Items)
                {
                    OrderItem orderItem = new OrderItem
                    {
                        OrderId = ReceiptOrder.Id,
                        CourseId = item.Id,
                        Quantity = 1,
                        Cost = item.Cost
                    };

                    ReceiptOrder.OrderItems.Add(orderItem);

                     //div id = "centered" style = "margin: 0 auto; width:855px;" ></ div >
                    ReceiptBody += "<div style=\"width: 20%; float:left;\">" + item.Name + "</div>";
                    ReceiptBody += "<div style=\"width: 50%; float:left;\">" + item.Description + "</div>";
                    ReceiptBody += "<div style=\"width: 10%; float:left;\">" + orderItem.Quantity.ToString() + "</div>";
                    ReceiptBody += "<div style=\"width: 20%; float:left;\">" + orderItem.Cost.ToString("C") + "</div>";

                }

                OrderManager.Insert(ReceiptOrder);

                //horizontal rule
                ReceiptBody += "<div style=\"width: 100%;\"><hr style='background-color:#000000;border-width:0;color:#000000;height:1px;line-height:0;text-align:left;width:100%;'/></div>";

                //subtotal, tax, and total
                ReceiptBody += "<div style=\"width: 80%;\"></div><div style=\"font-weight: bold; width: 20%; float:right;\">Sub Total: " + cart.SubTotal.ToString("C") + "</div><br />";
                ReceiptBody += "<div style=\"width: 80%;\"></div><div style=\"font-weight: bold; width: 20%; float:right;\">Tax: " + cart.Tax.ToString("C") + "</div><br />"; 
                ReceiptBody += "<div style=\"width: 80%;\"></div><div style=\"font-weight: bold; width: 20%; float:right;\">Total: " +  cart.Total.ToString("C") + "</div><br />";

                ReceiptBody += "</div>";

                string ReceiptEmail = ReceiptCustomer.Email;

                string ReceiptSubject = ReceiptCustomer.FirstName + ", Elevate thanks you for your purchase! Order #" + ReceiptOrder.Id.ToString();
                
                EmailService.SendReceiptEmail(ReceiptEmail, ReceiptSubject, ReceiptBody);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
