using BingoWebApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace BingoWebApp.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = null!;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }

        public DateTime? CreatedAt { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Role must be a valid value.")]
        public int Role { get; set; }
    }
}
