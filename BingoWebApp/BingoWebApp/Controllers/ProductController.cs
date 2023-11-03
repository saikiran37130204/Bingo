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
        /*
        public async Task<ActionResult<List<Product>>> DisplayProducts()
        {
            try
            {
                var products = await _product.GetAllProducts();
                if (products != null)
                {
                    return View(products);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }*/
    }
}
