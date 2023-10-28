using BingoWebApp.Entities;

using System.ComponentModel.DataAnnotations;

namespace BingoWebApp.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Username is required")]
        public  string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public  string Password { get; set; } = null!;

        public  string? Email { get; set; }
    }
}
