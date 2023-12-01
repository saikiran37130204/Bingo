namespace BingoWebApp.Models
{
    public class OrderItems
    {
        public class Order
        {
            public int OrderId { get; set; }
            public int? UserId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalPrice { get; set; }
            public int Status { get; set; }
            public List<OrderItem> OrderItems { get; set; }
        }

        public class OrderItem
        {
            public int OrderItemId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public string? ImageUrl { get; set; }
            public string? Category { get; set; }
            public int StockQuantity { get; set; }
            public int Quantity { get; set; }
            public decimal SubtotalPrice { get; set; }
            public decimal PriceWithTax { get; set; }
        }

    }
}
