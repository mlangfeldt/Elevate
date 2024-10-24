using System.ComponentModel.DataAnnotations;

namespace BL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string ResetCode { get; set; }
        public DateTime? ResetCodeExpiration { get; set; }
    }


}
