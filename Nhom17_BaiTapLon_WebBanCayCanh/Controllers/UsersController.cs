using Microsoft.AspNetCore.Mvc;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
