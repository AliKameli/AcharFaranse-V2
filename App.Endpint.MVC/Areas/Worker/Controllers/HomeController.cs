using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Worker.Controllers;

[Area("Worker")]
[Authorize(Roles = "Worker")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}