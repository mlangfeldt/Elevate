using Elevate.BL.Models;

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

                foreach (Course course in cart.Items)
                {

                    Collection collection = new Collection
                    {
                        CourseId = course.Id,
                        UserId = ReceiptCustomer.UserId
                    };

                    CollectionManager.Insert(collection);
                }

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
                }

                OrderManager.Insert(ReceiptOrder);

                string ReceiptBody =

                ReceiptBody = "<div id='header' style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; width: 1000px; margin: 0 auto;'>";

                //greeting
                ReceiptBody += "<br /><br /><div style='margin: auto; font-size: 24pt; font-weight: bold;'>" + ReceiptCustomer.FirstName + ", Elevate thanks you for your order!</div><br /><br />";

                //order information
                ReceiptBody += "<div style='border-radius: 15px; color:#ffffff; float: left; width: 100%; background-image: linear-gradient(to right, rgba(0, 0, 0, 1) 0%, rgba(13, 110, 253, 1) 100%); padding: 10px 10px 10px 10px;'>";
                ReceiptBody += "<div style='width: 50%; float: left;'><p style='font-weight: bold; margin:0;'>Elevate, inc.</p><p style='font-weight: bold;margin:0;'>1825 N Bluemound Dr</p><p style='font-weight: bold;margin:0;'>Appleton, WI 54914</p></div>";
                ReceiptBody += "<div id='centered' style = 'margin: 0 auto; width:100%;'>";
                ReceiptBody += "<div style='width: 50%; float: right;'><p style='font-weight: bold; margin:0;'>Order #: " + ReceiptOrder.Id.ToString("00000") +
                               "</p><p style='font-weight: bold; margin:0;'>Order date: " + ReceiptOrder.OrderDate.ToString("MM/dd/yyyy hh:mm tt") + "</p><p style='font-weight: bold; margin:0;'>Payment method: PayPal</p></div></div></div>";

                ReceiptBody += "<br /><br /><br /><br /><br />";

                ReceiptBody += "<div id='centered' style='padding: 5px 5px 5px 5px; font-size: 12pt; margin: 0 auto; width:100%;'>";

                // header
                ReceiptBody += "<div style=\"font-weight: bold; width: 20%; float:left;\">" + "Course Name" + "</div>";
                ReceiptBody += "<div style=\"font-weight: bold; width: 50%; float:left;\">" + "Description" + "</div>";
                ReceiptBody += "<div style=\"font-weight: bold; width: 10%; float:left;\">" + "Quantity" + "</div>";
                ReceiptBody += "<div style=\"font-weight: bold; width: 20%; float:left;\">" + "Cost" + "</div>";

                // horizontal rule
                ReceiptBody += "<div style='width: 100%;'><hr style='background-color:#c0c0c0;border-width:0;color:#000000;height:1px;line-height:0;text-align:left;width:100%;'/></div>";

                foreach (Course item in cart.Items)
                {
                    OrderItem orderItem = new OrderItem
                    {
                        OrderId = ReceiptOrder.Id,
                        CourseId = item.Id,
                        Quantity = 1,
                        Cost = item.Cost
                    };

                    ReceiptBody += "<div style='width: 20%; float:left'>" + item.Name + "</div>";
                    ReceiptBody += "<div style='width: 50%; float:left'>" + item.Description + "</div>";
                    ReceiptBody += "<div style='width: 10%; float:left'>" + orderItem.Quantity.ToString() + "</div>";
                    ReceiptBody += "<div style='width: 20%; float:left'>" + orderItem.Cost.ToString("C") + "</div>";

                }

                //horizontal rule
                ReceiptBody += "<div style='width: 100%;'><hr style='background-color:#c0c0c0;border-width:0;color:#000000;height:1px;line-height:0;text-align:left;width:100%;'/></div>";

                //subtotal, tax, and total
                ReceiptBody += "<div style='font-size: 12pt; width: 80%;'></div><div style='font-weight: bold; width: 20%; float:right;'>Sub Total: " + cart.SubTotal.ToString("C") + "</div><br />";
                ReceiptBody += "<div style='font-size: 12pt; width: 80%;'></div><div style='font-weight: bold; width: 20%; float:right;'>Tax: " + cart.Tax.ToString("C") + "</div><br />";
                ReceiptBody += "<div style='font-size: 12pt; width: 80%;'></div><div style='font-weight: bold; width: 20%; float:right;'>Total: " + cart.Total.ToString("C") + "</div><br />";

                ReceiptBody += "<br /><br />";

                //footer
                ReceiptBody += "</div><div style='font-size: 12pt; width: 100%; margin: auto; font-weight: normal;'> &copy; 2024 Elevate, inc.</div></div>";

                string ReceiptEmail = ReceiptCustomer.Email;

                string ReceiptSubject = ReceiptCustomer.FirstName + ", Elevate thanks you for your purchase! Order #" + ReceiptOrder.Id.ToString("00000");

                EmailService.SendReceiptEmail(ReceiptEmail, ReceiptSubject, ReceiptBody);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
