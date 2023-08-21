using System.ComponentModel.DataAnnotations;

namespace DataLayerCustomerManagement.DTOs
{
    public class UserDto
    {
        [StringLength(50, ErrorMessage = "Password cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [StringLength(50, ErrorMessage = "Password cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
