using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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