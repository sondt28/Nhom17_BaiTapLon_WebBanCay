using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using Nhom17_BaiTapLon_WebBanCayCanh.Services;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmAccount(string userId, string code)
        {
            
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmAccount" : "Error");
        }
        public IActionResult ConfirmAccountConfirmation()
        {
            return View();
        }
        public IActionResult ResetPassword(string userId, string code)
        {
            
            string email = _userManager.FindByIdAsync(userId).Result.Email;
            ViewData["Code"] = code;
            ViewData["Email"] = email;

            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmAccount", "Account",
                                        new { userId = user.Id, code = code },
                                        protocol: HttpContext.Request.Scheme);
                    var from = "son286202@gmail.com";
                    var subject = "Confirm account";
                    string text = "Confirm account by clicking here: <a href=\"" + callbackUrl + "\">link</a>";

                    MailSender.SendMail(from, "son286202@gmail.com", subject, text);

                    Order order = new Order();
                    order.UserId = user.Id;
                    order.Status = OrderStatus.INIT;
                    order.Date = DateTime.Now;
                    OrderDao.CreateOrder(_configuration, order);  

                    return RedirectToAction("ConfirmAccountConfirmation", "Account");
                }
                AddErrors(result);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", 
                                        new {userId = user.Id, code = code}, 
                                        protocol: HttpContext.Request.Scheme);

            var from = "son286202@gmail.com";
            var subject = "Reset password";
            string text = "Reset password by clicking here: <a href=\""+ callbackUrl + "\">link</a>";

            MailSender.SendMail(from, model.Email, subject, text);

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
