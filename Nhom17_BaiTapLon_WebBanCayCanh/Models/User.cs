using Microsoft.AspNetCore.Identity;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class User : IdentityUser
    {
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
