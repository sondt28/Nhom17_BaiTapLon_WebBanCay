namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class Shipping
    {
        public int Id { get; set; }
        public string ShippingMethod { get; set; }
        public decimal ShippingCost { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
