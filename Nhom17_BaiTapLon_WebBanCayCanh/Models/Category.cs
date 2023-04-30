using System.ComponentModel.DataAnnotations;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category> ChildCategories { get; set; }
        public List<Product>? Products { get; set; }
    }
}
