using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IConfiguration _configuration;
        public CategoriesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.Categories = CategoryDao.GetCategoies(_configuration);

            return View(categoryViewModel);
        }
        public JsonResult Create()
        {
            List<Category> categories = CategoryDao.GetCategoriesWithoutProduct(_configuration);
            CategoryViewModel model = new CategoryViewModel();
            model.ParentCategoryList = categories.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();

            return new JsonResult(model);
        }
        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            Category category = new Category()
            {
                Name = model.Name, 
                ParentCategoryId = model.ParentCategoryId
            };
            CategoryDao.CreateCategory(_configuration, category);

            return RedirectToAction("Index", "Categories");
        }
    }
}
