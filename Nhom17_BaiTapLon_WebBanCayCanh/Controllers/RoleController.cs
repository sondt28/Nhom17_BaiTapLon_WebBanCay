using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public RoleController(RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            RoleViewModel model = RoleDao.GetRoles(_configuration);

            return View(model);
        }
        public IActionResult Edit(string id)
        {
            RoleViewModel model = RoleDao.getRole(_configuration, id);
            return new JsonResult(model);
        }
        public IActionResult Delete(string id)
        {
            RoleViewModel model = RoleDao.getRole(_configuration, id);
            return new JsonResult(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = model.Name });
            }
            return RedirectToAction("Index", "Role");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole() {
                    Id = model.Id,
                    Name = model.Name,
                    NormalizedName = model.Name.ToUpper(),
                    ConcurrencyStamp = model.ConcurrencyStamp
                };
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded) {
                    return RedirectToAction("Index", "Role");
                }
            }
            return RedirectToAction("Index", "Role");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Id = model.Id,
                    Name = model.Name,
                    NormalizedName = model.Name.ToUpper(),
                    ConcurrencyStamp = model.ConcurrencyStamp
                };

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
            }
            return RedirectToAction("Index", "Role");
        }

    }
}
