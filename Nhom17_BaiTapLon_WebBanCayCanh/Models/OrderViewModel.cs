namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int TotalOrderItems { get; set; }
    }
}
