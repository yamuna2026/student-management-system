using System.ComponentModel.DataAnnotations;

namespace StudentManagementAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // Admin or User
    }
}