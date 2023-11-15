using BingoWebApp.Entities;
using BingoWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BingoWebApp.Controllers
{
    public class ProductController : Controller
    {
        public ILogger<ProductController> _logger;
        public IProducts _product;
        public ProductController(ILogger<ProductController> logger,IProducts products)
        {
            _logger = logger;
            _product = products;
        }
        public async Task<IActionResult> InsertInToCart(int ProductId = 0)
        {
            var user = await _product.InsertInToCart(ProductId);
            if (user)
            {
                return View();
            }
            return View();
        }
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _product.GetCartItems();
            
                return View(cartItems);
            
        }

        
    }
}
