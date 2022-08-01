using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Worker.ViewComponents;

[Area("Worker")]
public class WorkerJobCommentViewComponent : ViewComponent
{
    private readonly ICommentAppService _commentAppService;


    public WorkerJobCommentViewComponent(ICommentAppService commentAppService)
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