using Microsoft.AspNetCore.Mvc;

namespace App.Endpoint.MVC.Areas.Admin.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
