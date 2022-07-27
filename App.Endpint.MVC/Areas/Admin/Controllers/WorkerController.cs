using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.Endpoint.MVC.Areas.Admin.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoint.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class WorkerController : Controller
{
    private readonly ICityAppService _cityAppService;
    private readonly ICommentAppService _commentAppService;
    private readonly IJobAppService _jobAppService;
    private readonly IJobPictureAppService _jobPictureAppService;
    private readonly IWorkerAppService _workerAppService;

    public WorkerController(
        ICityAppService cityAppService,
        IJobAppService jobAppService,
        IJobPictureAppService jobPictureAppService,
        ICommentAppService commentAppService,
        IWorkerAppService workerAppService)
    {
        _cityAppService = cityAppService;
        _jobAppService = jobAppService;
        _jobPictureAppService = jobPictureAppService;
        _commentAppService = commentAppService;
        _workerAppService = workerAppService;
    }

    public async Task<ActionResult> Index(string? name = null,
        string? nationalId = null,
        string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

        List<WorkerDto> model = new();

        if (name == null && nationalId == null)
            model = await _workerAppService.GetAllAsync();

        else
            model = await _workerAppService.SearchAsync(name, nationalId);

        return View(model);
    }

    public async Task<ActionResult> Details(int id,
        DetailPageTypeEnum pageType = 0,
        string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);
        var model = await _workerAppService.GetByIdAsync(id);

        ViewData["PageType"] = pageType;

        return View(model);
    }

    public async Task<ActionResult> ConfirmWorker(int id)
    {
        try
        {
            await _workerAppService.ConfirmAsync(id);

            return RedirectToAction(nameof(Details), new
            {
                id, internalMessage = "با موفقیت تایید شد"
            });
        }
        catch (Exception)
        {
            return RedirectToAction(nameof(Details),
                new
                {
                    id, internalMessage = "خطا ! در فرآیند تایید مشکلی به وجود آمد"
                });
        }
    }

    [HttpGet]
    public async Task<ActionResult> Add()
    {
        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

        var model = new WorkerDto();

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Add(WorkerDto model)
    {
        if (ModelState.IsValid)
            try
            {
                await _workerAppService.AddAsync(model);

                return RedirectToAction(nameof(Index)
                    , new {internalMessage = $"مشتری {model.FirstName + " " + model.LastName} با موفقیت ایجاد شد"});
            }
            catch (Exception e)
            {
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

        return View(model);
    }

    public async Task<ActionResult> Edit(int id)
    {
        var model = await _workerAppService.GetByIdAsync(id);
        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(int id, WorkerDto model)
    {
        ModelState.ClearValidationState(nameof(WorkerDto.Password));
        ModelState.MarkFieldValid(nameof(WorkerDto.Password));
        ModelState.ClearValidationState(nameof(WorkerDto.ConfirmPassword));
        ModelState.MarkFieldSkipped(nameof(WorkerDto.ConfirmPassword));
        if (ModelState.IsValid)
            try
            {
                model.Id = id;
                await _workerAppService.UpdateAsync(model);

                return RedirectToAction(nameof(Index)
                    , new {internalMessage = "مشتری با موفقیت ویرایش شد"});
            }
            catch (Exception e)
            {
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

        return View(model);
    }

    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _workerAppService.DeleteAsync(id);

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