using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class UserViewModel
    {
        public List<UserRole> Users { get; set; }
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        [Required]
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
