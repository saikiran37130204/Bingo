using BingoWebApp.Entities;
using BingoWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BingoWebApp.ViewComponents
{
    public class ProductsViewComponent:ViewComponent
    {
        public readonly IProducts _products;
        public readonly IUser _user;
        public ProductsViewComponent(IProducts IProducts,IUser user )
        {
            _products = IProducts;
            _user = user;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           List<Product> products = await _products.GetAllProducts();
            return View(products);
        }


       /*
        public async Task<IViewComponentResult> InsertInToCart(int productId)
        {
            var user = await _user.InsertInToCart(productId);
            if (user)
            {
                return View();
            }
            return View();
        }*/

    }
}
