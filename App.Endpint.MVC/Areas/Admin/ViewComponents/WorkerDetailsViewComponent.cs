using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.Endpoint.MVC.Areas.Admin.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Endpoint.MVC.Areas.Admin.ViewComponents;

[Area("Admin")]
public class WorkerDetailsViewComponent : ViewComponent
{
    private readonly ICommentAppService _commentAppService;
    private readonly IJobAppService _jobAppService;
    private readonly IJobPictureAppService _jobPictureAppService;


    public WorkerDetailsViewComponent (ICommentAppService commentAppService, IJobAppService jobAppService, IJobPictureAppService jobPictureAppService)
    {
        _commentAppService = commentAppService;
        _jobAppService = jobAppService;
        _jobPictureAppService = jobPictureAppService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int id , DetailPageTypeEnum pageType)
    {

        if (pageType == DetailPageTypeEnum.Comments)
        {
            var items = new List<CommentDto>(await _commentAppService.GetByWorkerIdAsync(id));

            return View("Comments", items);
        }

        if (pageType == DetailPageTypeEnum.Pictures)
        {
            var items = new List<JobPictureDto>(await _jobPictureAppService.GetByWorkerIdAsync(id));
            return View("Pictures", items);
        }

        if (pageType == DetailPageTypeEnum.Jobs)
        {
            var items = new List<JobDto>(await _jobAppService.GetByWorkerIdAsync(id));
            return View("Jobs", items);
        }

        return View();
    }
}