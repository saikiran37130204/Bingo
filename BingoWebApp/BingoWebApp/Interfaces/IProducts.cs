using BingoWebApp.Entities;
using BingoWebApp.Models;

namespace BingoWebApp.Interfaces
{
    public interface IProducts
    {
        public Task<List<Product>> GetAllProducts();
        public Task<bool> InsertInToCart(int ProductId);
        public Task<List<CartItems>> GetCartItems();
        public Task<bool> RemoveFromCart(int ProductId);
        public Task<List<Product>> GetSearchProducts(string searchTerm);
        public Task<bool> InsertInToOrders(int ProductId);
        public Task<OrderItems.Order> GetOrders();
    }
}
