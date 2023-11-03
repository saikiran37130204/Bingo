using BingoWebApp.Entities;

namespace BingoWebApp.Interfaces
{
    public interface IProducts
    {
        public Task<List<Product>> GetAllProducts();
        
    }
}
