using System;
using System.Collections.Generic;

namespace BingoWebApp.Entities
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
