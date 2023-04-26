using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class ProductOptionViewModel
    {
        public List<ProductOption> ProductOptions { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public Product Product { get; set; }
        public IFormFile ProfileImage { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
