using System.ComponentModel.DataAnnotations;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string AddressDetails { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string SubDistrict { get; set; }
        public string Note { get; set; }
        public User User { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
