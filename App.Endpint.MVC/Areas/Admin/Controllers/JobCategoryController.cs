using App.AppServices;
using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.Endpoint.MVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoint.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class JobCategoryController : Controller
{
    private readonly IJobCategoryAppService _jobCategoryAppService;

    public JobCategoryController(IJobCategoryAppService jobCategoryAppService)
    {
        _jobCategoryAppService = jobCategoryAppService;
    }

    public async Task<IActionResult> Index(string? name, string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

        var model = await _jobCategoryAppService.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(name)) model = model.Where(x => x.Name.Contains(name)).ToList();

        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var model = await _jobCategoryAppService.GetByIdAsync(id);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Add(int? parentId, bool ended = false)
    {
        ViewBag.ParentId  = parentId;
        ViewBag.ParentParentId = null;
        ViewBag.ParentTree = null;

        if (parentId is not null)
        {
            var parentJob = await _jobCategoryAppService.GetByIdAsync(parentId.Value);
            ViewBag.ParentParentId ??= parentJob.ParentJobCategoryId;
            ViewBag.ParentTree = $"{parentJob.GroupPath}/{parentJob.Name}";
        }

        if (ended) return View(new JobCategoryDto(){});

        var model = await _jobCategoryAppService.GetByParentIdAsync(parentId);

        return View("AddParentSelect", model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(JobCategoryDto model, int? parentId)
    {
        if (ModelState.IsValid)
            try
            {
                model.ParentJobCategoryId = parentId;
                await _jobCategoryAppService.AddAsync(model);
                return RedirectToAction(nameof(Index)
                        , new { internalMessage = "با موفقیت ایجاد شد" });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _jobCategoryAppService.GetByIdAsync(id);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(JobCategoryDto model, int id)
    {

        if (ModelState.IsValid)
            try
            {
                model.Id = id;
                await _jobCategoryAppService.UpdateAsync(model);
                return RedirectToAction(nameof(Index)
                    , new { internalMessage = "با موفقیت ویرایش شد" });
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
            await _jobCategoryAppService.DeleteAsync(id);
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