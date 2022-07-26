using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class JobWorkerProposalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
