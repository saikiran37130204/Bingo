using System;
using System.Collections.Generic;

namespace BingoWebApp.Entities
{
    public partial class DriverLocation
    {
        public int LocationId { get; set; }
        public int? DriverId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual User? Driver { get; set; }
    }
}
