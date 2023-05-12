using Microsoft.AspNetCore.Mvc;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index(int id)
        {
            Order order = OrderDao.GetOrderDetails(_configuration, id);
            OrderViewModel model = new OrderViewModel()
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems,
            };
            return View(model);
        }
        public IActionResult Checkout(int orderId)
        {
            Order order = OrderDao.GetOrderDetails(_configuration, orderId);

            return View();
        }
    }
}
