using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Security.Claims;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    [Authorize]
    public class OrderItemController : Controller
    {
        private readonly IConfiguration _configuration;
        public OrderItemController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(OrderItemViewModel model)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order order = OrderDao.GetOrder(_configuration, userId);

            OrderItem orderItem = new OrderItem()
            {
                ProductId = model.ProductId,
                ProductOptionId = model.ProductOptionId ?? 0,
                OrderId = order.Id,
                Quantity = model.Quantity,
                Price = model.Price,
            };

            OrderItemDao.SaveOrderItem(_configuration, orderItem);

            return RedirectToAction("ProductDetailsPage", "Products", new
            {
                productId = model.ProductId,
                productOptionId = model.ProductOptionId
            });
        }
        public IActionResult Delete(int orderItemId, int order)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Address address = AddressDao.GetAddressByUserId(_configuration, userId);
            OrderItemDao.DeleteOrderItem(_configuration, orderItemId);

            return RedirectToAction("Index", "Order", new {id = order});
        }
    }
}
