using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Worker.Controllers;

[Area("Worker")]
[Authorize(Roles = "Worker")]
public class ProposalController : Controller
{
    private readonly IJobWorkerProposalAppService _jobWorkerProposalAppService;
    private readonly UserManager<IdentityUser<int>> _userManager;

    public ProposalController(IJobWorkerProposalAppService jobWorkerProposalAppService,
        UserManager<IdentityUser<int>> userManager)
    {
        _jobWorkerProposalAppService = jobWorkerProposalAppService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
        var userId = user.Id;
        var model = await _jobWorkerProposalAppService.GetByWorkerIdAsync(userId);
        model = model.OrderBy(x => x.ProposalStatus).ToList();

        return View(model);
    }

    [HttpGet]
    public IActionResult Add(int jobId)
    {
        var model = new JobWorkerProposalDto
        {
            JobId = jobId
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(JobWorkerProposalDto model, int jobId)
    {
        model.JobId = jobId;
        if (ModelState.IsValid)
            try
            {
                var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
                var userId = user.Id;
                model.WorkerId = userId;
                await _jobWorkerProposalAppService.AddAsync(model);

                return RedirectToAction(nameof(Index)
                    , new
                    {
                        internalMessage = "با موفقیت ایجاد شد"
                    });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        return View(model);
    }

    public async Task<ActionResult> Delete(int proposalId)
    {
        try
        {
            await _jobWorkerProposalAppService.DeleteAsync(proposalId);

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