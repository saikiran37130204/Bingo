using BingoWebApp.Entities;
using BingoWebApp.Models;

namespace BingoWebApp.Interfaces
{
    public interface IProducts
    {
        public Task<List<Product>> GetAllProducts();
        public Task<bool> InsertInToCart(int ProductId);
        public Task<List<CartItems>> GetCartItems();
    }
}
