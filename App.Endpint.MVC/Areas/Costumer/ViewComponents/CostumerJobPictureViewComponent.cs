using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Costumer.ViewComponents;

[Area("Costumer")]
public class CostumerJobPictureViewComponent : ViewComponent
{
    private readonly IJobPictureAppService _jobPictureAppService;

    public CostumerJobPictureViewComponent(IJobPictureAppService jobPictureAppService)
    {
        _jobPictureAppService = jobPictureAppService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int jobId)
    {
        var model = new List<JobPictureDto>(await _jobPictureAppService.GetByJobIdAsync(jobId));

        ViewData["JobId"] = jobId;

        return View(model);
    }
}