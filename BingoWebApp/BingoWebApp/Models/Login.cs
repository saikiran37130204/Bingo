//using BingoWebApp.Entities;

namespace BingoWebApp.Models
{
    public class Login
    {
        public  string Username { get; set; } = null!;

        public  string Password { get; set; } = null!;

        public  string? Email { get; set; }
    }
}
