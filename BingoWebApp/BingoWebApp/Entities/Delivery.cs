using System;
using System.Collections.Generic;

namespace BingoWebApp.Entities
{
    public partial class Delivery
    {
        public int DeliveryId { get; set; }
        public int? OrderId { get; set; }
        public int? DriverId { get; set; }
        public int DeliveryStatus { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }
        public DateTime? ActualDeliveryTime { get; set; }
        public string DeliveryAddress { get; set; } = null!;

        public virtual User? Driver { get; set; }
        public virtual Order? Order { get; set; }
    }
}
