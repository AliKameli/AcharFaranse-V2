using App.AppServices;
using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace App.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class JobController : Controller
    {
        private readonly IJobAppService _jobAppService;
        private readonly ICostumerAddressAppService _costumerAddressAppService;
        private readonly ICostumerAppService _costumerAppService;
        private readonly ICityAppService _cityAppService;
        private readonly IWorkerAppService _workerAppService;
        private readonly IJobCategoryAppService _jobCategoryAppService;


        public JobController(IJobAppService jobAppService, ICostumerAddressAppService costumerAddressAppService, ICostumerAppService costumerAppService, ICityAppService cityAppService, IWorkerAppService workerAppService, IJobCategoryAppService jobCategoryAppService)
        {
            _jobAppService = jobAppService;
            _costumerAddressAppService = costumerAddressAppService;
            _costumerAppService = costumerAppService;
            _cityAppService = cityAppService;
            _workerAppService = workerAppService;
            _jobCategoryAppService = jobCategoryAppService;
        }

        public async Task<ActionResult> Index(string? internalMessage = null)
        {
            if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

            var model = await _jobAppService.GetAllAsync();

            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await _jobAppService.GetByIdAsync(id);

            return View(model);
        }

        public async Task<ActionResult> AddChooseCity()
        {
            ViewData["Cities"] = new List<CityDto>(await _cityAppService.GetAllAsync());

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddChooseJobCategory(int cityId)
        {
            ViewData["JobCategories"] = new List<JobCategoryDto>(await _jobCategoryAppService.GetAllAsync());
            TempData["cityId"]=cityId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddChooseCostumer(int jobCategoryId)
        {
            ViewData["Costumers"] = new List<CostumerDto>(await _costumerAppService.GetByCityIdAsync((int)TempData.Peek("cityId")!));
            TempData["jobCategoryId"] = jobCategoryId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddChooseCostumerAddress(int costumerId)
        {
            ViewData["CostumerAddresses"] = new List<CostumerAddressDto>(await _costumerAddressAppService.GetByCostumerIdAsync(costumerId));
            TempData["costumerId"] = costumerId;
            return View();
        }


        [HttpPost]
        public ActionResult AddChosenAll(int costumerAddressId)
        {
            TempData["costumerAddressId"] = costumerAddressId;
            var model = new JobDto();
            return View("Add",model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(JobDto model)
        {
            if (ModelState.IsValid)
                try
                {
                    model.CostumerId= ((int)TempData["costumerId"]!);
                    model.CostumerAddressId = ((int) TempData["costumerAddressId"]!);
                    model.JobCategoryId= ((int)TempData["jobCategoryId"]!);
                    model.JobCityId= ((int)TempData["cityId"]!);
                    await _jobAppService.AddAsync(model);
                    return RedirectToAction(nameof(Index)
                        , new { internalMessage = "با موفقیت ایجاد شد" });
                }
                catch (Exception e)
                {
                    TempData.Keep();
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
                await _jobAppService.DeleteAsync(id);
                return RedirectToAction(nameof(Index)
                    , new { internalMessage = "با موفقیت حذف شد" });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index)
                    , new { internalMessage = "خطا : " + e.Message });
            }
        }
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _jobAppService.GetByIdAsync(id);
            ViewData["CostumerAddresses"] = new List<CostumerAddressDto>(await _costumerAddressAppService.GetByCostumerIdAsync(model.CostumerId));
            ViewData["JobCategories"] = new List<JobCategoryDto>(await _jobCategoryAppService.GetAllAsync());
            ViewData["Workers"] = new List<WorkerDto>(await _workerAppService.GetByCityIdAsync(model.JobCityId));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, JobDto model)
        {
            if (ModelState.IsValid)
                try
                {
                    model.Id = id;
                    await _jobAppService.UpdateAsync(model);
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
    }
}
