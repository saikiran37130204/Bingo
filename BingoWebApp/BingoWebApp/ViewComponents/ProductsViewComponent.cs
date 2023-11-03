using BingoWebApp.Entities;
using BingoWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BingoWebApp.ViewComponents
{
    public class ProductsViewComponent:ViewComponent
    {
        public readonly IProducts _IProducts;
        public ProductsViewComponent(IProducts IProducts)
        {
            _IProducts = IProducts;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           List<Product> products = await _IProducts.GetAllProducts();
            return View(products);
        }
    }
}
