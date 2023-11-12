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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly ILogger<ProductService> _logger;
        public ProductService(BingoDbContext dbContext, IHttpContextAccessor httpContextAccessor, ILogger<ProductService> logger)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<bool> InsertInToCart(int productId)
        {
            var product = await _dbContext.Products.Where(i => i.ProductId == productId).FirstOrDefaultAsync();
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
            if (!userId.HasValue && product == null)
            {
                return false;
            }
            else
            {
                await _dbContext.Carts.AddAsync(new Cart()
                {
                    UserId = userId,
                    ProductId = product?.ProductId,
                    CreatedDate = DateTime.Now,
                    Quantity = 1
                });
                var result = await _dbContext.SaveChangesAsync();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<List<CartItems>> GetCartItems()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
            var cartItems = await (from P in _dbContext?.Products
                                   join C in _dbContext?.Carts on P.ProductId equals C.ProductId
                                   where C.UserId == userId
                                   orderby C.CreatedDate descending // Order by the CreatedDate in descending order, adjust as needed
                                   select new CartItems
                                   {
                                       ProductId = P.ProductId,
                                       ProductName = P.ProductName,
                                       Price = P.Price,
                                       UserId = C.UserId,
                                       ImageUrl = P.ImageUrl,
                                       Quantity = C.Quantity,
                                       Description = P.Description
                                   }).ToListAsync();
                return cartItems;   
        }

       
    }
}
