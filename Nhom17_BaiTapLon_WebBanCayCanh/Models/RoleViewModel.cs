using System.ComponentModel.DataAnnotations;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class RoleViewModel
    {
        public List<Role>? Roles { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Id { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
