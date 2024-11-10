using System.ComponentModel.DataAnnotations;

namespace Elevate.BL.Models
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
        [Compare("NewPassword", ErrorMessage = "passwords do not match")]
        public string? ConfirmNewPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ResetCode { get; set; }
        public DateTime ResetCodeExpiration { get; set; }

        public int EmailConfirmed { get; set; }
        public string ConfirmationCode { get; set; }
    }


}
