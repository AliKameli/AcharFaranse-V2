using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoint.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class CityController : Controller
{
    private readonly ICityAppService _cityAppService;
    private readonly IWorkerAppService _workerAppService;

    public CityController(ICityAppService cityAppService, IWorkerAppService workerAppService)
    {
        _cityAppService = cityAppService;
        _workerAppService = workerAppService;
    }

    public async Task<ActionResult> Index(string? name = null, string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

        var model = new List<CityDto>();

        if (!string.IsNullOrWhiteSpace(name))
            model = await _cityAppService.SearchByNameAsync(name);
        else
            model = await _cityAppService.GetAllAsync();

        return View(model);
    }

    public async Task<ActionResult> Details(int id)
    {
        var model = await _cityAppService.GetByIdAsync(id);
        ViewData["Workers"] = new List<WorkerDto>(await _workerAppService.GetByCityIdAsync(id));
        return View(model);
    }

    public ActionResult Add()
    {
        var model = new CityDto();
        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Add(CityDto model)
    {
        if (ModelState.IsValid)
            try
            {
                await _cityAppService.AddAsync(model);
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

    public async Task<ActionResult> Edit(int id)
    {
        var model = await _cityAppService.GetByIdAsync(id);
        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(int id, CityDto model)
    {
        if (ModelState.IsValid)
            try
            {
                model.Id = id;
                await _cityAppService.UpdateAsync(model);
                return RedirectToAction(nameof(Index)
                    , new {internalMessage = "با موفقیت ویرایش شد"});
            }
            catch (Exception e)
            {
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        return View(model);
    }

    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _cityAppService.DeleteAsync(id);
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