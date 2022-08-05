using App.AppServices;
using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.EndPoint.MVC.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    [Authorize(Roles = "Costumer")]
    public class ProfileController : Controller
    {
        private readonly ICostumerAppService _costumerAppService;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly ICityAppService _cityAppService;

        public ProfileController(ICostumerAppService costumerAppService, UserManager<IdentityUser<int>> userManager, ICityAppService cityAppService)
        {
            _costumerAppService = costumerAppService;
            _userManager = userManager;
            _cityAppService = cityAppService;
        }

        public async Task<IActionResult> Index(string? internalMessage = null)
        {
            if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

            ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());
            var model = await _costumerAppService.GetByIdAsync(
                (await _userManager.FindByNameAsync(User!.Identity!.Name!)).Id);
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
                        , new { internalMessage = "با موفقیت ویرایش شد" });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
                }
            else
                ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

            ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

            return View("Index",model);
        }

        [HttpGet]
        public IActionResult EditPicture(int costumerId)
        {
            var model = new CostumerDto()
            {
                Id = costumerId
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPicture(CostumerDto model,int costumerId)
        {
            model.Id = costumerId;
            if (ModelState["PictureFile"] is null || (ModelState["PictureFile"]?.ValidationState == ModelValidationState.Valid))
                try
                {
                    await _costumerAppService.EditPictureAsync(model);

                    return RedirectToAction(nameof(Index)
                        , new { internalMessage = "با موفقیت تغییر کرد شد" });
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

}
