using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.EndPoint.MVC.Models.Enum;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable Mvc.ViewComponentViewNotResolved

namespace App.Endpoint.MVC.Areas.Admin.ViewComponents;

[Area("Admin")]
public class JobCatChooseViewComponent : ViewComponent
{
    private readonly ICommentAppService _commentAppService;
    private readonly ICostumerAddressAppService _costumerAddressAppService;
    private readonly IJobAppService _jobAppService;
    private readonly IJobPictureAppService _jobPictureAppService;


    public JobCatChooseViewComponent(ICostumerAddressAppService costumerAddressAppService,
        ICommentAppService commentAppService,
        IJobAppService jobAppService,
        IJobPictureAppService jobPictureAppService)
    {
        _costumerAddressAppService = costumerAddressAppService;
        _commentAppService = commentAppService;
        _jobAppService = jobAppService;
        _jobPictureAppService = jobPictureAppService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int id, DetailPageTypeEnum pageType)
    {
        if (pageType == DetailPageTypeEnum.Addresses)
        {
            var items =
                new List<CostumerAddressDto>(await _costumerAddressAppService.GetByCostumerIdAsync(id));

            return View("Addresses", items);
        }

        if (pageType == DetailPageTypeEnum.Comments)
        {
            var items = new List<CommentDto>(await _commentAppService.GetByCostumerIdAsync(id));

            return View("Comments", items);
        }

        if (pageType == DetailPageTypeEnum.Pictures)
        {
            var items = new List<JobPictureDto>(await _jobPictureAppService.GetByCostumerIdAsync(id));

            return View("Pictures", items);
        }

        if (pageType == DetailPageTypeEnum.Jobs)
        {
            var items = new List<JobDto>(await _jobAppService.GetByCostumerIdAsync(id));

            return View("Jobs", items);
        }

        return View();
    }
}