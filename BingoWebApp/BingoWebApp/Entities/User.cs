using System;
using System.Collections.Generic;

namespace BingoWebApp.Entities
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Deliveries = new HashSet<Delivery>();
            DriverLocations = new HashSet<DriverLocation>();
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int Role { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<DriverLocation> DriverLocations { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
