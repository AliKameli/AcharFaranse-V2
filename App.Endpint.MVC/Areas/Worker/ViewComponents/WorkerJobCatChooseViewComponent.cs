using App.Domain.Contracts.AppService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Worker.ViewComponents;

[Area("Worker")]
public class WorkerJobCatChooseViewComponent : ViewComponent
{
    private readonly IJobCategoryAppService _jobCategoryAppService;
    private readonly UserManager<IdentityUser<int>> _userManager;
    public WorkerJobCatChooseViewComponent(IJobCategoryAppService jobCategoryAppService, UserManager<IdentityUser<int>> userManager)
    {
        _jobCategoryAppService = jobCategoryAppService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? parentId = null)
    {
        var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
        int userId = user.Id;
        var allCats = await _jobCategoryAppService.GetAllAsync();
        var workerCats = await _jobCategoryAppService.GetByWorkerIdAsync(userId);
        var model = allCats.Where(x => workerCats.All(y => y.Id != x.Id)).ToList();
        ViewData["ParentId"] = parentId;

        return View(model);
    }
}