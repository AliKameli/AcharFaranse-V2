using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Costumer.Controllers;

[Area("Costumer")]
[Authorize(Roles = "Costumer")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}