using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class JobPictureController : Controller
{
    private readonly IJobPictureAppService _jobPictureAppService;


    public JobPictureController(IJobPictureAppService jobPictureAppService)
    {
        _jobPictureAppService = jobPictureAppService;
    }

    public async Task<ActionResult> Index(string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

        var model = await _jobPictureAppService.GetAllAsync();

        return View(model);
    }

    public async Task<ActionResult> Details(int id, string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);


        var model = await _jobPictureAppService.GetByIdAsync(id);

        return View(model);
    }

    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _jobPictureAppService.DeleteAsync(id);

            return RedirectToAction(nameof(Index)
                , new {internalMessage = "با موفقیت حذف شد"});
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index)
                , new {internalMessage = "خطا : " + e.Message});
        }
    }

    public async Task<ActionResult> ConfirmPicture(int id)
    {
        try
        {
            await _jobPictureAppService.ConfirmAsync(id);

            return RedirectToAction(nameof(Details), new
            {
                id, internalMessage = "با موفقیت تایید شد"
            });
        }
        catch (Exception)
        {
            return RedirectToAction(nameof(Details), new
            {
                id, internalMessage = "خطا ! در فرآیند تایید مشکلی به وجود آمد"
            });
        }
    }

    public ActionResult Add()
    {
        var model = new JobPictureDto();

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Add(JobPictureDto model)
    {
        if (ModelState.IsValid)
            try
            {
                model.CostumerId = 1;
                model.JobId = 1;
                model.UserType = UserTypeEnum.Customer;
                await _jobPictureAppService.AddAsync(model);

                return RedirectToAction(nameof(Index)
                    , new {internalMessage = "با موفقیت ایجاد شد"});
            }
            catch (Exception e)
            {
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        return View(model);
    }
}