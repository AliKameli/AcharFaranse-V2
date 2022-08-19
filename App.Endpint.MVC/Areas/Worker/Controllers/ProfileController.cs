using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.EndPoint.MVC.Areas.Worker.Controllers;

[Area("Worker")]
[Authorize(Roles = "Worker")]
public class ProfileController : Controller
{
    private readonly ICityAppService _cityAppService;
    private readonly UserManager<IdentityUser<int>> _userManager;
    private readonly IWorkerAppService _workerAppService;

    public ProfileController(IWorkerAppService workerAppService,
        UserManager<IdentityUser<int>> userManager,
        ICityAppService cityAppService)
    {
        _workerAppService = workerAppService;
        _userManager = userManager;
        _cityAppService = cityAppService;
    }

    public async Task<IActionResult> Index(string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());
        var model = await _workerAppService.GetByIdAsync(
            (await _userManager.FindByNameAsync(User!.Identity!.Name!)).Id);

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
                    , new {internalMessage = "با موفقیت ویرایش شد"});
            }
            catch (Exception e)
            {
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

        return View("Index", model);
    }

    [HttpGet]
    public IActionResult EditPicture(int workerId)
    {
        var model = new WorkerDto
        {
            Id = workerId
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditPicture(WorkerDto model, int workerId)
    {
        model.Id = workerId;
        if (ModelState["PictureFile"] is null ||
            ModelState["PictureFile"]?.ValidationState == ModelValidationState.Valid)
            try
            {
                await _workerAppService.EditPictureAsync(model);

                return RedirectToAction(nameof(Index)
                    , new {internalMessage = "با موفقیت تغییر کرد شد"});
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