using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.Endpoint.MVC.Areas.Admin.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace App.Endpoint.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CostumerController : Controller
{
    private readonly ICityAppService _cityAppService;
    private readonly ICommentAppService _commentAppService;
    private readonly ICostumerAddressAppService _costumerAddressAppService;
    private readonly ICostumerAppService _costumerAppService;
    private readonly IJobAppService _jobAppService;
    private readonly IJobPictureAppService _jobPictureAppService;

    public CostumerController(ICostumerAppService costumerAppService,
        ICityAppService cityAppService,
        ICostumerAddressAppService costumerAddressAppService,
        IJobAppService jobAppService,
        IJobPictureAppService jobPictureAppService,
        ICommentAppService commentAppService)
    {
        _costumerAppService = costumerAppService;
        _cityAppService = cityAppService;
        _costumerAddressAppService = costumerAddressAppService;
        _jobAppService = jobAppService;
        _jobPictureAppService = jobPictureAppService;
        _commentAppService = commentAppService;
    }

    public async Task<ActionResult> Index(string? name = null,
        string? nationalId = null,
        string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

        List<CostumerDto> model = new();

        if (name == null && nationalId == null)
            model = await _costumerAppService.GetAllAsync();

        else
            model = await _costumerAppService.SearchAsync(name, nationalId);

        return View(model);
    }

    public async Task<ActionResult> Details(int id, DetailPageTypeEnum pageType = 0,
        string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);
        var model = await _costumerAppService.GetByIdAsync(id);

        ViewData["PageType"] = pageType;
        return View(model);
    }

    public async Task<ActionResult> ConfirmCostumer(int id)
    {
        try
        {
            await _costumerAppService.ConfirmAsync(id);
            return RedirectToAction(nameof(Details), new { id = id, internalMessage="با موفقیت تایید شد" });
        }
        catch (Exception)
        {
            return RedirectToAction(nameof(Details), new { id = id, internalMessage = "خطا ! در فرآیند تایید مشکلی به وجود آمد" });
        }
    }

    [HttpGet]
    public async Task<ActionResult> Add()
    {
        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

        var model = new CostumerDto();

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Add(CostumerDto model)
    {
        if (ModelState.IsValid)
            try
            {
                await _costumerAppService.AddAsync(model);
                return RedirectToAction(nameof(Index)
                    , new { internalMessage = $"مشتری {model.FirstName + " " + model.LastName} با موفقیت ایجاد شد" });
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
        var model = await _costumerAppService.GetByIdAsync(id);
        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());
        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(int id, CostumerDto model)
    {
        ModelState.ClearValidationState(nameof(CostumerDto.Password));
        ModelState.MarkFieldValid(nameof(CostumerDto.Password));
        ModelState.ClearValidationState(nameof(CostumerDto.ConfirmPassword));
        ModelState.MarkFieldSkipped(nameof(CostumerDto.ConfirmPassword));
        if (ModelState.IsValid)
            try
            {
                model.Id = id;
                await _costumerAppService.UpdateAsync(model);
                return RedirectToAction(nameof(Index)
                    , new { internalMessage = "مشتری با موفقیت ویرایش شد" });
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
            await _costumerAppService.DeleteAsync(id);
            return RedirectToAction(nameof(Index)
                , new { internalMessage = "با موفقیت حذف شد" });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index)
                , new { internalMessage = "خطا : " + e.Message });
        }
    }
}