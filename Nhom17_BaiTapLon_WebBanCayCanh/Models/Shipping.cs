namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class Shipping
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public decimal Cost { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
