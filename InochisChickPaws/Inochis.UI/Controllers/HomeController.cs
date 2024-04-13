using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inochis.Business.Abstract;

namespace Inochis.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productManager;

        public HomeController(IProductService productManager)
        {
            _productManager = productManager;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productManager.GetAllNonDeletedAsync(false);
            return View(products.Data);
        }
        public async Task<IActionResult> Contact()
        {
            var products = await _productManager.GetAllNonDeletedAsync(false);
            return View(products.Data);
        }
        public async Task<IActionResult> Hakkimizda()
        {
            var products = await _productManager.GetAllNonDeletedAsync(false);
            return View(products.Data);
        }
    }
}
