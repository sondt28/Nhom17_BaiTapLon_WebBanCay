using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal TotalAmount { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public OrderStatus Status { get; set; }
        public int Count { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
    public enum OrderStatus
    {
        INIT,
        Pending,
        Processing,
        Shipped,
        Delivired
    }
}
