using App.Domain.Contracts.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class JobWorkerProposalController : Controller
{
    private readonly IJobAppService _jobAppService;
    private readonly IJobWorkerProposalAppService _jobWorkerProposalAppService;

    public JobWorkerProposalController(IJobWorkerProposalAppService jobWorkerProposalAppService,
        IJobAppService jobAppService)
    {
        _jobWorkerProposalAppService = jobWorkerProposalAppService;
        _jobAppService = jobAppService;
    }

    public async Task<ActionResult> Index(string? internalMessage = null)
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
                , new {internalMessage = "با موفقیت حذف شد"});
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index)
                , new {internalMessage = "خطا : " + e.Message});
        }
    }
}