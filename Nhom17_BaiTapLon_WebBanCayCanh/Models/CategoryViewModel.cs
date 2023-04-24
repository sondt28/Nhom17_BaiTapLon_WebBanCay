using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class CategoryViewModel
    {
        public List<Category>? Categories { get; set; }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Danh mục cha")]
        public int ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Product>? Products { get; set; }
        
        public IEnumerable<SelectListItem> ParentCategoryList { get; set; }
    }
}
