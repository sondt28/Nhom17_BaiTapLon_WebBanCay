using System.ComponentModel.DataAnnotations;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Models
{
    public class RegisterViewModel
    {
        [EmailAddress(ErrorMessage = "Email required")]
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Passowrd")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
