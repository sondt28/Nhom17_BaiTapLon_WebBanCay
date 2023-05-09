using System.ComponentModel.DataAnnotations;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class OrderItemViewModel
    {
        public List<OrderItem> OrderItems { get; set; }
        public int Id { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Quantity must be between 1 and 5.")]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int? ProductOptionId { get; set; }
        public ProductOption ProductOption { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        

    }
}
