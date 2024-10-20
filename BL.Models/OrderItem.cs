namespace BL.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CourseId { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
    }
}
