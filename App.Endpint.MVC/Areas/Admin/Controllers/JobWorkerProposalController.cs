using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace App.EndPoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class JobWorkerProposalController : Controller
    {
        private readonly IJobWorkerProposalAppService _jobWorkerProposalAppService;
        private readonly IJobAppService _jobAppService;

        public JobWorkerProposalController(IJobWorkerProposalAppService jobWorkerProposalAppService, IJobAppService jobAppService)
        {
            _jobWorkerProposalAppService = jobWorkerProposalAppService;
            _jobAppService = jobAppService;
        }

        public async Task<ActionResult> Index( string? internalMessage = null)
        {
            if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

            var model = await _jobWorkerProposalAppService.GetAllAsync();

            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await _jobWorkerProposalAppService.GetByIdAsync(id);
            ViewData["Job"] = await _jobAppService.GetByIdAsync(model.JobId);
            return View(model);
        }


        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _jobWorkerProposalAppService.DeleteAsync(id);
                return RedirectToAction(nameof(Index)
                    , new { internalMessage = "با موفقیت حذف شد" });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index)
                    , new { internalMessage = "خطا : " + e.Message });
            }
        }
    }
}
