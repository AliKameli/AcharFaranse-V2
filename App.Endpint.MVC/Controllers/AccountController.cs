using App.Endpoint.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoint.MVC.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser<int>> _signInManager;
    private readonly UserManager<IdentityUser<int>> _userManager;

    public AccountController(
        UserManager<IdentityUser<int>> userManager,
        SignInManager<IdentityUser<int>> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Login()
    {
        await _signInManager.SignOutAsync();
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded) return LocalRedirect("~/");

            ModelState.AddModelError(string.Empty, "خطا در فرآیند لاگین");
        }

        return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return LocalRedirect("~/");
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser<int>
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(user, "CustomerRole");
                await _signInManager.SignInAsync(user, false);
                return LocalRedirect("~/");
            }

            foreach (var item in result.Errors) ModelState.AddModelError(string.Empty, item.Description);
        }

        return View(model);
    }

    [AllowAnonymous]
    public IActionResult Manage()
    {
        return View();
    }
}