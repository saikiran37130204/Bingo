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
                return RedirectToAction("GetCartItems");
            }
            return View("Home","Index");
        }
        [HttpPost]
        public async Task<IActionResult> GetSearchProducts(string searchTerm)
        {
            var items = await _product.GetSearchProducts(searchTerm);
            return View("Views/Shared/Components/Products/Default.cshtml", items);
        }
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _product.GetCartItems();
            return View(cartItems);
        }

        public async Task<IActionResult> RemoveFromCart(int ProductId)
        {
            var cartItems= await _product.RemoveFromCart(ProductId);
            return RedirectToAction("GetCartItems");
        }

        public async Task<IActionResult> InsertInToOrders(int ProductId)
        {
            var order=await _product.InsertInToOrders(ProductId);
            if (order)
            {
                return RedirectToAction("GetCartItems");
            }
            return View();
        }
         
        public async Task<IActionResult> GetOrders()
        {
            var orderItems=await _product.GetOrders();
            return View(orderItems);
        }



    }
}
