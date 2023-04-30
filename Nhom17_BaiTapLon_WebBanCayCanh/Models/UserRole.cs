using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class UserRole
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string? RoleName { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
