using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        [DisplayName("Order Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        [DisplayName("Customer Name")]
        public string CustomerFullName { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Subtotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Tax
        {
            get
            {
                double tax = 0;
                tax = Subtotal * 0.055;
                return tax;
            }
        }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get; set; }
    }
}
