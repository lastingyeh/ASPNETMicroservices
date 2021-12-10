using System.ComponentModel.DataAnnotations;

namespace AuthServer.API.Models.Requests
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}