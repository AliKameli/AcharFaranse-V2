using App.AppServices;
using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.EndPoint.MVC.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Costumer.ViewComponents;

[Area("Costumer")]
public class CostumerJobCommentViewComponent : ViewComponent
{
    private readonly ICommentAppService _commentAppService;


    public CostumerJobCommentViewComponent(ICommentAppService commentAppService)
    {
        _commentAppService = commentAppService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int jobId)
    {
        var model = new List<CommentDto>(await _commentAppService.GetByJobIdAsync(jobId));

        ViewData["JobId"] = jobId;

        return View(model);
    }
}