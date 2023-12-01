using System;
using System.Collections.Generic;

namespace BingoWebApp.Entities
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int? OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PaymentMethod { get; set; }
        public decimal PaymentAmount { get; set; }
        public virtual Order? Order { get; set; }

    }
}
