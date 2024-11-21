using System.ComponentModel.DataAnnotations;

namespace Elevate.BL.Models
{
    public class ShoppingCart
    {
        public List<Course> Items { get; set; } = new List<Course>();
        public int NumberOfItems { get { return Items.Count; } }
        public int TotalCount { get { return Items.Count; } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double SubTotal
        {
            get
            {
                double total = 0;
                Items.ForEach(x => total += x.Cost);
                return total;
            }
        }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Tax { get { return SubTotal * .055; } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get { return SubTotal + Tax; } }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
    }
}

