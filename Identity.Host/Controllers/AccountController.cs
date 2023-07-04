using Identity.Domain.Entities;
using Identity.Host.ViewModels;

namespace Identity.Host.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        ViewData["ReturnUrl"] = model.ReturnUrl;

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}
