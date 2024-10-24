using Elevate.BL.Models;
using System.ComponentModel;

namespace Elevate.UI.ViewModels
{
    public class CustomerViewModel
    {
        [DisplayName("Customer")]
        public int CustomerId { get; set; }

        public List<Customer> Customers { get; set; }

        public int UserId { get; set; }

        public ShoppingCart Cart { get; set; }
    }
}
