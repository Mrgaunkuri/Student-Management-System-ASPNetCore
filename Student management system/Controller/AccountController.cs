using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Student_management_system.model;
using System.Threading.Tasks;

namespace Student_management_system.Controllers // ✅ FIXED: should be plural "Controllers"
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ✅ GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl; // ✅ FIXED: correct ViewData usage
            return View();
        }

        // ✅ POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // ✅ FIXED: moved outside result.Succeeded block
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
        //GEt Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //POST Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // <-- Add debug logging here
            Console.WriteLine("ModelState.IsValid: " + ModelState.IsValid);
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                    Console.WriteLine(state.Key + " => " + error.ErrorMessage);
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                   await _userManager.AddToRoleAsync(user, "Teacher");
                    return RedirectToAction("login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        // ✅ POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
