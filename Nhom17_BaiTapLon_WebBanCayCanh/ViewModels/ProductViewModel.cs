using Nhom17_BaiTapLon_WebBanCayCanh.Models;

namespace Nhom17_BaiTapLon_WebBanCayCanh.ViewModel
{
    public class ProductViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool Availability { get; set; }
        public IFormFile ProfileImage { get; set; }
        public int CategoryId { get; set; }
    }
}
