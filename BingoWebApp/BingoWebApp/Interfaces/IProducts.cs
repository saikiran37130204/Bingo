using BingoWebApp.Entities;

namespace BingoWebApp.Interfaces
{
    public interface IProducts
    {
        public Task<Product> GetAllProducts();
    }
}
