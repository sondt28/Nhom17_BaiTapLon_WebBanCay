using Nhom17_BaiTapLon_WebBanCayCanh.Models;

namespace Nhom17_BaiTapLon_WebBanCayCanh.ViewModels
{
    public class ProductWithCategories
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
