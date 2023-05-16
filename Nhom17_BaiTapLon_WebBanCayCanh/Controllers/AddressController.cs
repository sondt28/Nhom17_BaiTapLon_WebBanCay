using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Security.Claims;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    public class AddressController : Controller
    {
        private readonly IConfiguration _configuration;
        public AddressController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Create()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Address address = AddressDao.GetAddressByUserId(_configuration, userId);
            if (address != null)
            {
                AddressViewModel model = new AddressViewModel()
                {
                    Id = address.Id,
                    Name = address.Name,
                    PhoneNumber = address.PhoneNumber,
                    AddressDetails = address.AddressDetails,
                    District = address.District,
                    SubDistrict = address.SubDistrict,
                    City = address.City,
                    Note = address.Note,
                    UserId = address.UserId
                };

                return View(model);
            }

            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(AddressViewModel address)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Address model = new Address()
            {
                Id = address.Id,
                Name = address.Name,
                PhoneNumber = address.PhoneNumber,
                AddressDetails = address.AddressDetails,
                District = address.District,
                SubDistrict = address.SubDistrict,
                City = address.City,
                Note = address.Note,
                UserId = userId
            };

            AddressDao.SaveAddress(_configuration, model);

            return RedirectToAction("UserInfo", "User");
        }
    }
}
