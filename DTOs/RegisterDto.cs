using System.ComponentModel.DataAnnotations;

namespace dating_backend.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
