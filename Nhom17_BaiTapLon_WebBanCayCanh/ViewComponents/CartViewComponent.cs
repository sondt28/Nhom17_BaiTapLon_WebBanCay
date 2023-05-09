using Microsoft.AspNetCore.Mvc;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Security.Claims;

namespace Nhom17_BaiTapLon_WebBanCayCanh.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IConfiguration _configuration;

        public CartViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IViewComponentResult Invoke()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            OrderViewModel orderViewModel = new OrderViewModel();
            if (userId == null)
            {
                orderViewModel.TotalOrderItems = 0;
                return View(orderViewModel);
            }
            int count = OrderItemDao.GetTotalOfOrderItems(_configuration, userId);
            
            orderViewModel.TotalOrderItems = count;

            return View(orderViewModel);
        }
    }
}
