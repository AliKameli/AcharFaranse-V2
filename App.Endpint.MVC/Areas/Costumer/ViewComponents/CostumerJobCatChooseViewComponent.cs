using App.Domain.Contracts.AppService;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Costumer.ViewComponents;

[Area("Costumer")]
public class CostumerJobCatChooseViewComponent : ViewComponent
{
    private readonly IJobCategoryAppService _jobCategoryAppService;

    public CostumerJobCatChooseViewComponent(IJobCategoryAppService jobCategoryAppService)
    {
        _jobCategoryAppService = jobCategoryAppService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? parentId = null)
    {
        var model = await _jobCategoryAppService.GetAllAsync();
        ViewData["ParentId"] = parentId;

        return View(model);
    }
}