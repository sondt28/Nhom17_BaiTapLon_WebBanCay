namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressDetails { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public string Note { get; set; }
        public User User { get; set;}
        public int UserId { get; set; }
    }
}
