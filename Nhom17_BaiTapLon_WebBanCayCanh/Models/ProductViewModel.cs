using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [MaxLength(5000)]
        [Required]
        public string? Description { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }
        [Required]
        public bool Availability { get; set; }
        [Required]
        public IFormFile ProfileImage { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public List<ProductOption>? ProductOptions;
        public int? ProductOptionId { get; set; }
        
    }
}
