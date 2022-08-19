using App.Domain.Contracts.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Worker.Controllers;

[Area("Worker")]
[Authorize(Roles = "Worker")]
public class JobCategoryController : Controller
{
    private readonly IJobCategoryAppService _jobCategoryAppService;
    private readonly UserManager<IdentityUser<int>> _userManager;
    private readonly IWorkerAppService _workerAppService;

    public JobCategoryController(IJobCategoryAppService jobCategoryAppService,
        UserManager<IdentityUser<int>> userManager,
        IWorkerAppService workerAppService)
    {
        _jobCategoryAppService = jobCategoryAppService;
        _userManager = userManager;
        _workerAppService = workerAppService;
    }

    public async Task<IActionResult> Index(string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

        var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
        var userId = user.Id;
        var model = await _jobCategoryAppService.GetByWorkerIdAsync(userId);

        return View(model);
    }

    public ActionResult AddChooseJobCategory()
    {
        return View();
    }

    public async Task<ActionResult> Add(int jobCatId)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
            var userId = user.Id;
            await _workerAppService.AddToJobCategory(userId, jobCatId);

            return RedirectToAction(nameof(Index)
                , new {internalMessage = "با موفقیت اضافه شد"});
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index)
                , new {internalMessage = "خطا : " + e.Message});
        }
    }

    public async Task<ActionResult> Delete(int jobCatId)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
            var userId = user.Id;
            await _workerAppService.DeleteFromJobCategory(userId, jobCatId);

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