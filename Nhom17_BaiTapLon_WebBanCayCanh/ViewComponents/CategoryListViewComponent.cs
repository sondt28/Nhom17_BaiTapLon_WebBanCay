using Microsoft.AspNetCore.Mvc;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;

namespace Nhom17_BaiTapLon_WebBanCayCanh.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public CategoryListViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.Categories = GetCategories();

            return View(categoryViewModel);
        }
        private List<Category> GetCategories(int? parentId = null)
        {
            var categories = CategoryDao.GetSubCategoriesOfCategory(_configuration, parentId);

            foreach (var category in categories)
            {
                category.ChildCategories = GetCategories(category.Id);
            }

            return categories;
        }
    }
}
