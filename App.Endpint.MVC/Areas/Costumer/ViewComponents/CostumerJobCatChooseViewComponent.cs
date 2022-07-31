using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.Endpoint.MVC.Areas.Admin.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Costumer.ViewComponents;

[Area("Costumer")]
public class CostumerJobCatChooseViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(List<JobCategoryDto> jobCategories, int? parentId = null)
    {
        ViewData["ParentId"] = parentId;
        return View();
    }
}