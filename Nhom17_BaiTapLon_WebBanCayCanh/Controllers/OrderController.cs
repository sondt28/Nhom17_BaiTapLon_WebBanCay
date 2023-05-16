using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Security.Claims;

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
        [Authorize]
        public IActionResult Checkout(int orderId)
        {
            Order order = OrderDao.GetOrderDetails(_configuration, orderId);
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Address address = AddressDao.GetAddressByUserId(_configuration, userId);

            AddressAndOrderViewModel addressAndOrderViewModel = new AddressAndOrderViewModel()
            {
                AddressId = address.Id,
                Name = address.Name,
                PhoneNumber = address.PhoneNumber,
                AddressDetails = address.AddressDetails,
                City = address.City,
                District = address.District,
                SubDistrict = address.SubDistrict,
                Note = address.Note,
                UserId = userId,
                OrderId = order.Id,
                Date = order.Date,
                TotalAmount = order.TotalAmount,
                Count = order.Count,
                OrderItems = order.OrderItems
            };

            return View(addressAndOrderViewModel);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Checkout(AddressAndOrderViewModel addressAndOrderViewModel)
        {
            Order order = new Order()
            {
                Id = addressAndOrderViewModel.OrderId,
                TotalAmount = addressAndOrderViewModel.TotalAmount,
                Date = DateTime.Now,
                Status = OrderStatus.Pending
            };
            Address address = new Address()
            {
                City = addressAndOrderViewModel.City,
                District = addressAndOrderViewModel.District,
                SubDistrict = addressAndOrderViewModel.SubDistrict,
                AddressDetails = addressAndOrderViewModel.AddressDetails,
                Note = addressAndOrderViewModel.Note,
                Name = addressAndOrderViewModel.Name,
                PhoneNumber = addressAndOrderViewModel.PhoneNumber,
                UserId = addressAndOrderViewModel.UserId
            };

            AddressDao.SaveAddress(_configuration, address);
            OrderDao.Checkout(_configuration, order);

            return RedirectToAction("CheckoutConfirmation");
        }
        public IActionResult CheckoutConfirmation()
        {
            return View();
        }
        public IActionResult HistoryOrder()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Order> orders = OrderDao.GetHistoryOrdersByUserId(_configuration, userId);
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.Orders = orders;
            
            return View(orderViewModel);
        }
        public IActionResult GetOrders()
        {
            List<Order> orders = OrderDao.GetOrders(_configuration);
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.Orders = orders;

            return View(orderViewModel);
        }
        public IActionResult OrderDetails(int id)
        {
            Order orderDetails = OrderDao.GetOrderDetails(_configuration, id);
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Address address = AddressDao.GetAddressByUserId(_configuration, userId);
            Order order = OrderDao.GetOrderById(_configuration, id);

            AddressAndOrderViewModel addressAndOrderViewModel = new AddressAndOrderViewModel()
            {
                AddressId = address.Id,
                Name = address.Name,
                PhoneNumber = address.PhoneNumber,
                AddressDetails = address.AddressDetails,
                City = address.City,
                District = address.District,
                SubDistrict = address.SubDistrict,
                Note = address.Note,
                UserId = userId,
                OrderId = orderDetails.Id,
                Order = order,
                Date = orderDetails.Date,
                TotalAmount = orderDetails.TotalAmount,
                Count = orderDetails.Count,
                OrderItems = orderDetails.OrderItems
            };
            
            return View(addressAndOrderViewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateStatusOrder(Order order)
        {
            OrderDao.UpdateStatusOrder(_configuration, order);

            return RedirectToAction("OrderDetails", new {id = order.Id});
        }
    }
}
