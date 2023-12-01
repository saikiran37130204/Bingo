using BingoWebApp.Entities;
using BingoWebApp.Interfaces;
using BingoWebApp.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Product>> GetSearchProducts(string searchTerm)
        {
            try
            {
                if (searchTerm != null)
                {
                    // Filter products based on the search term
                    var products = await _dbContext.Products
                        .Where(p => p.ProductName.Contains(searchTerm))
                        .ToListAsync();

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
        public async Task<bool> RemoveFromCart(int ProductId)
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");

            if (userId != null)
            {
                var cartItem = await _dbContext.Carts
                    .Where(c => c.ProductId == ProductId && c.UserId == userId)
                    .FirstOrDefaultAsync();

                if (cartItem != null)
                {
                    _dbContext.Carts.Remove(cartItem);
                    var result = await _dbContext.SaveChangesAsync();
                    return result != 0;
                }
            }

            return false;
        }

        /* 
         public async Task<bool> InsertInToOrders(int ProductId)
         {
             using (var transaction = _dbContext.Database.BeginTransaction())
             {
                 try
                 {
                     var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");

                     // Validate user and product
                     if (userId <= 0 || ProductId <= 0)
                     {
                         return false;
                     }

                     // Retrieve the product from the database
                     var product = await _dbContext.Products
                         .Where(p => p.ProductId == ProductId).FirstOrDefaultAsync();

                     // Check if the specified productId is valid
                     if (product == null)
                     {
                         return false;
                     }
                     DateTime currentDate = DateTime.Now.Date; 

                     var orders = await _dbContext.Orders
                         .Where(o => o.OrderDate.Date == currentDate)
                         .FirstOrDefaultAsync();
                     // Create a new order entity
                     var order = new Order
                     {
                         UserId = userId,
                         OrderDate = DateTime.Now,
                         OrderItems = new List<OrderItem>
                 {
                     new OrderItem
                     {
                         ProductId = product.ProductId,
                         Quantity = 1, // You might adjust this based on your business logic
                         SubtotalPrice = product.Price + product.Price * 8 / 100,  // You might adjust this based on your business logic
                     }
                 }
                     };

                     // Add the order to the database
                     await _dbContext.Orders.AddAsync(order);

                     // Remove the item from the cart
                     var cart = await RemoveFromCart(ProductId);
                     if (cart != null)
                     {
                         // Commit the transaction
                         await _dbContext.SaveChangesAsync();
                         await transaction.CommitAsync();

                         return true;
                     }

                     // If removal from cart fails, rollback the transaction
                     await transaction.RollbackAsync();
                     return false;
                 }
                 catch (Exception ex)
                 {
                     // Log the exception or handle it according to your application's needs
                     _logger.LogError(ex, "Error occurred while creating an order.");

                     // Rollback the transaction in case of an exception
                     await transaction.RollbackAsync();
                     return false;
                 }
             }
         }*/
        public async Task<bool> InsertInToOrders(int ProductId)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
                
                if (userId <= 0 || ProductId <= 0)
                {
                    return false;
                }
                
                var product = await _dbContext.Products
                    .Where(p => p.ProductId == ProductId).FirstOrDefaultAsync();

                
                if (product == null)
                {
                    return false;
                }
                
                DateTime currentDate = DateTime.Now.Date;

                var existingOrder = await _dbContext.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId && o.OrderDate.Date == currentDate)
                    .FirstOrDefaultAsync();

                if (existingOrder != null)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId=existingOrder.OrderId,
                        ProductId = ProductId,
                        Quantity = 1, 
                        SubtotalPrice = product.Price + product.Price * 8 / 100, 
                    };

                    await _dbContext.OrderItems.AddAsync(orderItem);
                    existingOrder.TotalPrice += orderItem.SubtotalPrice;
                }
                else
                {
                    var newOrder = new Order
                    {
                        UserId = userId,
                        OrderDate = currentDate,
                        
                        OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = product.ProductId,
                        Quantity = 1,
                        SubtotalPrice = product.Price + product.Price * 8 / 100,
                    }
                    
                },
                        TotalPrice = product.Price + product.Price * 8 / 100,
                    };

                    await _dbContext.Orders.AddAsync(newOrder);
                }

                var cart = await RemoveFromCart(ProductId);
                if (cart==true)
                {
                    var result = await _dbContext.SaveChangesAsync();
                    if(result>=0)
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating an order.");
                return false;
            }
        }

        public async Task<OrderItems.Order> GetOrders()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
                DateTime currentDate = DateTime.Now.Date;

                var query = from order in _dbContext.Orders
                            where order.UserId == userId && order.OrderDate.Date == currentDate
                            join orderItem in _dbContext.OrderItems on order.OrderId equals orderItem.OrderId
                            join product in _dbContext.Products on orderItem.ProductId equals product.ProductId
                            select new
                            {
                                order,
                                orderItem,
                                product
                            };

                var groupedOrders = query.GroupBy(o => o.order.OrderId)
                                        .Select(group => new OrderItems.Order
                                        {
                                            OrderId = group.Key,
                                            UserId = userId,
                                            OrderDate = group.First().order.OrderDate,
                                            TotalPrice = group.First().order.TotalPrice,
                                            Status = group.First().order.Status,
                                            OrderItems = group.Select(og => new OrderItems.OrderItem
                                            {
                                                OrderItemId = og.orderItem.OrderItemId,
                                                ProductId = og.product.ProductId,
                                                ProductName = og.product.ProductName,
                                                Description = og.product.Description,
                                                PriceWithTax = og.orderItem.SubtotalPrice,
                                                ImageUrl = og.product.ImageUrl,
                                                Quantity = og.orderItem.Quantity
                                            }).ToList()
                                        });

                var result = await groupedOrders.FirstOrDefaultAsync();
                if (result != null)
                {
                    result.TotalPrice = result.OrderItems.Sum(item => item.PriceWithTax);
                }

                // Further processing or return the result as needed
                return result;
            }
            catch (Exception ex)
            {
                // Handle the exception or log it based on your application's needs
                _logger.LogError(ex, "Error occurred while retrieving order items.");
                return null; // Or return an empty list or handle the error accordingly
            }
        }


    }
}
