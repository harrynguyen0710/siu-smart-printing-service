using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using siu_smart_printing_service.Models;
using siu_smart_printing_service.ViewModels;

namespace siu_smart_printing_service.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        
        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewData["returnUrl"] = ReturnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.password, false, false);

            if (result.Succeeded) 
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Account");

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        
    }
}
