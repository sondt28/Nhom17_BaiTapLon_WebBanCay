namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class ProductOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }
        public int StockQuantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
