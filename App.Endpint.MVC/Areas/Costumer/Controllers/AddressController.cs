using App.AppServices;
using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace App.EndPoint.MVC.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    [Authorize(Roles = "Costumer")]
    public class AddressController : Controller
    {
        private readonly ICostumerAddressAppService _costumerAddressAppService;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly ICityAppService _cityAppService;

        public AddressController(ICostumerAddressAppService costumerAddressAppService, UserManager<IdentityUser<int>> userManager, ICityAppService cityAppService)
        {
            _costumerAddressAppService = costumerAddressAppService;
            _userManager = userManager;
            _cityAppService = cityAppService;
        }

        public async Task<IActionResult> Index(string? internalMessage = null)
        {
            if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

            var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
            var model = (await _costumerAddressAppService.GetByCostumerIdAsync(user.Id))
                .Where(x=>!x.IsDeleted)
                .ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            var model = new CostumerAddressDto();
            ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(CostumerAddressDto model)
        {
            if (ModelState.IsValid)
                try
                {
                    var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
                    model.CostumerId = user.Id;
                    await _costumerAddressAppService.AddAsync(model);

                    return RedirectToAction(nameof(Index)
                        , new { internalMessage = "با موفقیت ایجاد شد" });
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

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _costumerAddressAppService.GetByIdAsync(id);
            ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, CostumerAddressDto model)
        {
            if (ModelState.IsValid)
                try
                {
                    model.Id = id;
                    await _costumerAddressAppService.UpdateAsync(model);

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
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _costumerAddressAppService.DeleteAsync(id);

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
}
