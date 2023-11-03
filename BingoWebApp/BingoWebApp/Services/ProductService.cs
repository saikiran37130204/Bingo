using BingoWebApp.Entities;
using BingoWebApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BingoWebApp.Models;

namespace BingoWebApp.Services
{
    public class ProductService : IProducts
    {
        public readonly BingoDbContext _dbContext;
        public readonly ILogger<ProductService> _logger;
        public ProductService(BingoDbContext dbContext,ILogger<ProductService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        

        public Task<List<Product>> GetAllProducts()
        {
            try
            {
                if (_dbContext.Products != null)
                {
                    var products = _dbContext.Products.ToListAsync();
                    return products;
                }

            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
            return null;
        }

        
        
    }
}
