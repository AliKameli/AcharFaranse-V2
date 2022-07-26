using App.Endpoint.MVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkerController : Controller
    {

        private readonly ICityAppService _cityAppService;
        private readonly IWorkerAppService _workerAppService;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public WorkerController(IWorkerAppService workerAppService, ICityAppService cityAppService,
            UserManager<IdentityUser<int>> userManager)
        {
            _workerAppService = workerAppService;
            _cityAppService = cityAppService;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index(string? name)
        {
            var record = await _workerAppService.GetAllAsync();


            if (!string.IsNullOrWhiteSpace(name))
                record = record.Where(x =>
                        x.FirstName.Contains(name)
                        || x.LastName.Contains(name)
                        || new string(x.FirstName + ' ' + x.LastName).Contains(name))
                    .ToList();

            var model = record.Select(x => new WorkerVM
                {
                    Id = x.Id,
                    Email = _userManager.FindByIdAsync(x.Id.ToString()).Result.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    NationalSecurityId = x.NationalSecurityId,
                    IsConfirmed = x.IsConfirmed,
                    UserCityName = x.UserCityId is not null
                        ? _cityAppService.GetByIdAsync((int) x.UserCityId).Result.CityName
                        : null,
                    UserCityId = x.UserCityId
                })
                .ToList();

            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var record = await _workerAppService.GetByIdAsync(id);
            var user = await _userManager.FindByIdAsync(record!.Id.ToString());
            var model = new WorkerVM
            {
                Id = record!.Id,
                CreationDateTime = record.CreationDateTime,
                LastUpdateDateTime = record.LastUpdateDateTime,
                Email = user.Email,
                FirstName = record.FirstName,
                LastName = record.LastName,
                NationalSecurityId = record.NationalSecurityId,
                PhoneNumber = user.PhoneNumber,
                HomeAddress = record.HomeAddress,
                Description = record.Description,
                IsConfirmed = record.IsConfirmed,
                ConfirmDateTime = record.ConfirmDateTime,
                UserCityName = record.UserCityId is not null
                    ? _cityAppService.GetByIdAsync((int) record.UserCityId)
                        .Result.CityName
                    : null,
            };

            return View(model);
        }

        public async Task<ActionResult> ConfirmWorker(int id)
        {
            await _workerAppService.ConfirmWorker(id);
            return RedirectToAction(nameof(Details), routeValues: new {id = id});
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            ViewBag.Cities = (await _cityAppService.GetAllAsync())
                .Select(x => new CityVM
                {
                    Id = x.Id,
                    CityName = new string(x.CityName + " / " + x.StateName)
                })
                .ToList();

            var model = new WorkerVM();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(WorkerVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser<int>
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                IdentityResult? result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    try
                    {
                        var record = new WorkerDto
                        {
                            Id = int.Parse(await _userManager.GetUserIdAsync(user)),
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            NationalSecurityId = model.NationalSecurityId,
                            HomeAddress = model.HomeAddress,
                            UserCityId = model.UserCityId,
                            Description = model.Description
                        };
                        await _workerAppService.AddAsync(record);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception e)
                    {
                        await _userManager.DeleteAsync(user);
                        ModelState.AddModelError(string.Empty, e.Message);
                    }
                }

                foreach (var item in result.Errors) ModelState.AddModelError(string.Empty, item.Description);
            }

            ViewBag.Cities = (await _cityAppService.GetAllAsync())
                .Select(x => new CityVM
                {
                    Id = x.Id,
                    CityName = new string(x.CityName + " / " + x.StateName)
                })
                .ToList();
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var record = await _workerAppService.GetByIdAsync(id);
            var user = await _userManager.FindByIdAsync(record!.Id.ToString());
            var model = new WorkerVM
            {
                Id = record!.Id,
                Email = user.Email,
                FirstName = record.FirstName,
                LastName = record.LastName,
                PhoneNumber = user.PhoneNumber,
                NationalSecurityId = record.NationalSecurityId,
                HomeAddress = record.HomeAddress,
                UserCityId = record.UserCityId,
                Description = record.Description,
            };
            ViewBag.Cities = (await _cityAppService.GetAllAsync())
                .Select(x => new CityVM
                {
                    Id = x.Id,
                    CityName = new string(x.CityName + " / " + x.StateName)
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, WorkerVM model)
        {
            List<string> errors = new List<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    var record = await _workerAppService.GetByIdAsync(id);

                    record!.FirstName = model.FirstName;
                    record.LastName = model.LastName;
                    record.NationalSecurityId = model.NationalSecurityId;
                    record.HomeAddress = model.HomeAddress;
                    record.UserCityId = model.UserCityId;
                    record.Description = model.Description;

                    var user = await _userManager.FindByIdAsync(id.ToString());
                    var oldEmail = user.Email;
                    if (user.Email != model.Email)
                    {
                        user.UserName = model.Email;
                        user.Email = model.Email;
                        var emailChangeResult = await _userManager.UpdateAsync(user);
                        errors.AddRange(emailChangeResult.Errors.Select(x => x.Description).ToList());
                    }

                    if (errors.Any())
                    {
                        throw new Exception();
                    }

                    try
                    {
                        await _workerAppService.UpdateAsync(record);
                    }
                    catch (Exception e)
                    {
                        if (user.Email != oldEmail)
                        {
                            user.UserName = oldEmail;
                            user.Email = oldEmail;
                            await _userManager.UpdateAsync(user);
                            await _userManager.UpdateNormalizedUserNameAsync(user);
                            await _userManager.UpdateNormalizedEmailAsync(user);
                        }

                        errors.Add(e.Message);
                        errors.Add(e.InnerException!.Message);
                        throw;
                    }

                    if (user.PhoneNumber != model.PhoneNumber)
                    {
                        await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                        user.PhoneNumber = model.PhoneNumber;
                    }

                    if (model.Password == "no change")
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }

                    return RedirectToAction(nameof(Index));

                }
                catch
                {

                }
                finally
                {
                    errors.ForEach(x => ModelState.AddModelError(string.Empty, x));
                }
            }

            ViewBag.Cities = (await _cityAppService.GetAllAsync())
                .Select(x => new CityVM
                {
                    Id = x.Id,
                    CityName = new string(x.CityName + " / " + x.StateName)
                })
                .ToList();
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return RedirectToAction(nameof(Index));

            await _workerAppService.DeleteAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
