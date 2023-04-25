using Microsoft.AspNetCore.Identity;
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
        public IActionResult Update(int id)
        {
            Category category = CategoryDao.GetCategory(_configuration, id);
            List<Category> categories = CategoryDao.GetCategoriesWithoutProductAndOwnCategory(_configuration, id);
            CategoryViewModel model = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategory.Id,
                ParentCategory = category.ParentCategory,
                ParentCategoryList = categories.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }).ToList()
            };

            return new JsonResult(model);
        }
        [HttpPost]
        public IActionResult Update(CategoryViewModel model)
        {
            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name,
                ParentCategoryId = model.ParentCategoryId
            };
            CategoryDao.UpdateCategory(_configuration, category);

            return RedirectToAction("Index", "Categories");
        }
        public IActionResult Delete(int id)
        {
            Category category = CategoryDao.GetCategory(_configuration, id);
            CategoryViewModel model = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name
            };
            return new JsonResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            
            CategoryDao.DeleteCategory(_configuration, model.Id);

            return RedirectToAction("Index", "Categories");
        }
        public IActionResult Details(int id)
        {
            Category category = CategoryDao.GetCategory(_configuration, id);
            CategoryViewModel model = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategory.Id,
                ParentCategory = category.ParentCategory
            };
            return new JsonResult(model);
        }
    }
}
