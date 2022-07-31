using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.Endpoint.MVC.Areas.Admin.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Costumer.ViewComponents;

[Area("Costumer")]
public class JobPictureViewComponent : ViewComponent
{
    private readonly IJobPictureAppService _jobPictureAppService;

    public JobPictureViewComponent(IJobPictureAppService jobPictureAppService)
    {
        _jobPictureAppService = jobPictureAppService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int jobId)
    {
        var model = new List<JobPictureDto>(await _jobPictureAppService.GetByJobIdAsync(jobId));

        return View(model);
    }
}