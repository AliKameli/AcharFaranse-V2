using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.Endpoint.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoint.MVC.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly ICityAppService _cityAppService;
    private readonly ICostumerAppService _costumerAppService;
    private readonly ILogger _logger;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly SignInManager<IdentityUser<int>> _signInManager;
    private readonly UserManager<IdentityUser<int>> _userManager;
    private readonly IWorkerAppService _workerAppService;

    public AccountController(
        SignInManager<IdentityUser<int>> signInManager,
        RoleManager<IdentityRole<int>> roleManager,
        UserManager<IdentityUser<int>> userManager,
        ICityAppService cityAppService,
        ICostumerAppService costumerAppService,
        IWorkerAppService workerAppService,
        ILogger<AccountController> logger)
    {
        _signInManager = signInManager;
        _roleManager = roleManager;
        _userManager = userManager;
        _cityAppService = cityAppService;
        _costumerAppService = costumerAppService;
        _workerAppService = workerAppService;
        _logger = logger;
    }

    public async Task<IActionResult> Login()
    {
        await _signInManager.SignOutAsync();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result =
                await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {ModelUserName} Logged In Successfully", model.UserName);

                return await GoToUserHomePage(model.UserName);
            }

            ModelState.AddModelError("internalMessage", "خطا در فرآیند لاگین");
            _logger.LogWarning("Wrong username and password Used to login");
        }
        else
        {
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");
            _logger.LogError("Invalid Input for login");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(Index), "Home", new {area = ""});
    }

    public async Task<IActionResult> Register()
    {
        await _signInManager.SignOutAsync();

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CostumerRegister()
    {
        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

        var model = new CostumerDto();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CostumerRegister(CostumerDto model)
    {
        if (ModelState.IsValid)
            try
            {
                await _costumerAppService.AddAsync(model);

                return await Login(new LoginViewModel
                {
                    UserName = model.Email,
                    Password = model.Password,
                    RememberMe = false
                });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> WorkerRegister()
    {
        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

        var model = new WorkerDto();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> WorkerRegister(WorkerDto model)
    {
        if (ModelState.IsValid)
            try
            {
                await _workerAppService.AddAsync(model);

                return await Login(new LoginViewModel
                {
                    UserName = model.Email,
                    Password = model.Password,
                    RememberMe = false
                });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

        return View(model);
    }

    public IActionResult Manage()
    {
        return View();
    }

    private async Task<IActionResult> GoToUserHomePage(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (await _userManager.IsInRoleAsync(user, "Admin"))
            return RedirectToAction(nameof(Index), "Home", new {area = "Admin"});

        if (await _userManager.IsInRoleAsync(user, "Costumer"))
            return RedirectToAction(nameof(Index), "Home", new {area = "Costumer"});

        if (await _userManager.IsInRoleAsync(user, "Worker"))
            return RedirectToAction(nameof(Index), "Home", new {area = "Worker"});

        return null!;
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}