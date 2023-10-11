using System;
using System.Collections.Generic;

namespace BingoWebApp.Entities
{
    public partial class Order
    {
        public Order()
        {
            Deliveries = new HashSet<Delivery>();
            OrderItems = new HashSet<OrderItem>();
            Payments = new HashSet<Payment>();
        }

        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
