using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Worker.ViewComponents;

[Area("Worker")]
public class WorkerJobProposalViewComponent : ViewComponent
{
    private readonly IJobWorkerProposalAppService _jobWorkerProposalAppService;
    private readonly UserManager<IdentityUser<int>> _userManager;


    public WorkerJobProposalViewComponent(IJobWorkerProposalAppService jobWorkerProposalAppService, UserManager<IdentityUser<int>> userManager)
    {
        _jobWorkerProposalAppService = jobWorkerProposalAppService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(int jobId)
    {
        var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
        int userId = user.Id;
        var model = new List<JobWorkerProposalDto>(await _jobWorkerProposalAppService.GetByJobIdAsync(jobId));

        ViewData["JobId"] = jobId;
        ViewData["WorkerId"] = userId;

        return View(model);
    }
}