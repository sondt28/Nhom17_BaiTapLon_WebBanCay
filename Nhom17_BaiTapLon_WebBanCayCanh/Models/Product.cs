namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? Image { get; set; }
        public bool Availability { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<ProductOption>? ProductOptions { get; set; } 
    }
}
