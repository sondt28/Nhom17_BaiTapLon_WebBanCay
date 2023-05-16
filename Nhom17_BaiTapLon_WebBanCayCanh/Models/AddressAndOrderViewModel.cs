namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class AddressAndOrderViewModel
    {
        public int AddressId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressDetails { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public string Note { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime? Date { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public int Count { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
