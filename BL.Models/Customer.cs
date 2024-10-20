namespace Elevate.BL.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }

    }
}
