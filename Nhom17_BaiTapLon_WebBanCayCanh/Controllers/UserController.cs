using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult UserInfo()
        {
            return View();
        }
        public IActionResult Index()
        {
            UserViewModel model = UserDao.getUserRoles(_configuration);
            return View(model);
        }
        public IActionResult Edit(string id)
        {
            UserViewModel model = UserDao.getUser(_configuration, id);
            List<Role> roles = RoleDao.GetRoles1(_configuration);
            model.RoleList = roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return new JsonResult(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var roleOfUser = await _userManager.GetRolesAsync(user);
            if (roleOfUser.Count > 0)
            {
                await _userManager.RemoveFromRolesAsync(user, roleOfUser);
            }
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                var updateResult = await _userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            return new JsonResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> ManagerUserClaims(string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return null;
        }

    }
}
