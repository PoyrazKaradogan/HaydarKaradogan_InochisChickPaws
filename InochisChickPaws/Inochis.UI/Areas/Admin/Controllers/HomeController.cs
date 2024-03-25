using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Inochis.Business.Abstract;

namespace Inochis.UI.Areas.Admin.Controllers
{
    [Authorize(Roles ="SuperAdmin, Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IOrderService _orderManager;

        public HomeController(IOrderService orderManager)
        {
            _orderManager = orderManager;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderManager.GetOrdersAsync();
            orders = orders.Take(5).ToList();
            return View(orders);
        }
    }
}
