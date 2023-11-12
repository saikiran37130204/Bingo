using BingoWebApp.Entities;

namespace BingoWebApp.Models
{
    public class CartItems
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public int StockQuantity { get; set; }
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Quantity { get; set; }



    }
}
