using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using App.Domain.Enums;
using App.EndPoint.MVC.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Costumer.Controllers;

[Area("Costumer")]
[Authorize(Roles = "Costumer")]
public class JobController : Controller
{
    private readonly ICityAppService _cityAppService;
    private readonly ICommentAppService _commentAppService;
    private readonly ICostumerAddressAppService _costumerAddressAppService;
    private readonly ICostumerAppService _costumerAppService;
    private readonly IJobAppService _jobAppService;
    private readonly IJobCategoryAppService _jobCategoryAppService;
    private readonly IJobPictureAppService _jobPictureAppService;
    private readonly IJobWorkerProposalAppService _jobWorkerProposalAppService;
    private readonly UserManager<IdentityUser<int>> _userManager;
    private readonly IWorkerAppService _workerAppService;

    public JobController(IJobAppService jobAppService,
        ICostumerAddressAppService costumerAddressAppService,
        ICostumerAppService costumerAppService,
        ICityAppService cityAppService,
        IWorkerAppService workerAppService,
        IJobCategoryAppService jobCategoryAppService,
        UserManager<IdentityUser<int>> userManager,
        IJobWorkerProposalAppService jobWorkerProposalAppService,
        IJobPictureAppService jobPictureAppService,
        ICommentAppService commentAppService)
    {
        _jobAppService = jobAppService;
        _costumerAddressAppService = costumerAddressAppService;
        _costumerAppService = costumerAppService;
        _cityAppService = cityAppService;
        _workerAppService = workerAppService;
        _jobCategoryAppService = jobCategoryAppService;
        _userManager = userManager;
        _jobWorkerProposalAppService = jobWorkerProposalAppService;
        _jobPictureAppService = jobPictureAppService;
        _commentAppService = commentAppService;
    }

    public async Task<ActionResult> Index(string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

        var model = await _jobAppService.GetByUserNameAsync(User?.Identity?.Name!);

        model = model.OrderBy(x => x.JobStatus).ToList();

        return View(model);
    }

    public async Task<ActionResult> Details(int id,
        DetailPageTypeEnum pageType = 0,
        string? internalMessage = null)
    {
        if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

        var model = await _jobAppService.GetByIdAsync(id);

        ViewData["PageType"] = pageType;

        return View(model);
    }

    public ActionResult AddChooseJobCategory()
    {
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> Add(int jobCatId)
    {
        var model = new JobDto
        {
            JobCategoryId = jobCatId
        };
        var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
        var userId = user.Id;

        ViewData["Addresses"] = new List<CostumerAddressDto>(
            (await _costumerAddressAppService.GetByCostumerIdAsync(userId))
            .Where(x => !x.IsDeleted)
            .ToList());

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Add(JobDto model, int jobCatId)
    {
        var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
        var userId = user.Id;
        model.JobCategoryId = jobCatId;
        if (ModelState.IsValid)
            try
            {
                model.CostumerId = userId;
                model.JobCityId = _costumerAddressAppService.GetByIdAsync(model.CostumerAddressId).GetAwaiter()
                    .GetResult().CityId;
                await _jobAppService.AddAsync(model);

                return RedirectToAction(nameof(Index)
                    , new {internalMessage = "با موفقیت ایجاد شد"});
            }
            catch (Exception e)
            {
                TempData.Keep();
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        ViewData["Addresses"] = new List<CostumerAddressDto>(
            (await _costumerAddressAppService.GetByCostumerIdAsync(userId))
            .Where(x => !x.IsDeleted)
            .ToList());

        return View(model);
    }

    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _jobAppService.DeleteAsync(id, User?.Identity?.Name!);

            return RedirectToAction(nameof(Index)
                , new {internalMessage = "با موفقیت حذف شد"});
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index)
                , new {internalMessage = "خطا : " + e.Message});
        }
    }

    public async Task<ActionResult> ChangePaymentMethod(int id)
    {
        try
        {
            await _jobAppService.ChangePaymentMethod(id);

            return RedirectToAction(nameof(Index)
                , new {internalMessage = "با موفقیت تغییر کرد شد"});
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index)
                , new {internalMessage = "خطا : " + e.Message});
        }
    }

    public async Task<ActionResult> Proposals(int jobId)
    {
        var model = await _jobWorkerProposalAppService.GetByJobIdAsync(jobId);

        return View(model);
    }

    public async Task<ActionResult> AcceptProposal(int proposalId)
    {
        try
        {
            await _jobWorkerProposalAppService.AcceptAsync(proposalId);

            return RedirectToAction(nameof(Index)
                , new {internalMessage = "پیشنهاد با موفقیت انتخاب شد"});
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index)
                , new {internalMessage = "خطا : " + e.Message});
        }
    }

    public async Task<ActionResult> DeletePicture(int pictureId)
    {
        try
        {
            await _jobPictureAppService.DeleteAsync(pictureId);

            return RedirectToAction(nameof(Index)
                , new {internalMessage = "با موفقیت حذف شد"});
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index)
                , new {internalMessage = "خطا : " + e.Message});
        }
    }

    public ActionResult AddPicture(int jobId)
    {
        var model = new JobPictureDto
        {
            JobId = jobId
        };

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> AddPicture(JobPictureDto model, int jobId)
    {
        model.JobId = jobId;
        if (ModelState.IsValid)
            try
            {
                var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
                var userId = user.Id;
                model.CostumerId = userId;
                model.UserType = UserTypeEnum.Customer;
                await _jobPictureAppService.AddAsync(model);

                return RedirectToAction(nameof(Details)
                    , new
                    {
                        id = jobId,
                        internalMessage = "با موفقیت ایجاد شد"
                    });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("internalMessage", $"خطا ! {e.Message}");
            }
        else
            ModelState.AddModelError("internalMessage", "خطا ! ورودی پذیرفته نیست");

        return View(model);
    }

    public async Task<ActionResult> DeleteComment(int commentId)
    {
        try
        {
            await _commentAppService.DeleteAsync(commentId);

            return RedirectToAction(nameof(Index)
                , new {internalMessage = "با موفقیت حذف شد"});
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index)
                , new {internalMessage = "خطا : " + e.Message});
        }
    }

    public ActionResult AddComment(int jobId)
    {
        var model = new CommentDto
        {
            JobId = jobId
        };

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> AddComment(CommentDto model, int jobId)
    {
        model.JobId = jobId;
        if (ModelState.IsValid)
            try
            {
                var user = await _userManager.FindByNameAsync(User?.Identity?.Name!);
                var userId = user.Id;
                model.CostumerId = userId;
                model.UserType = UserTypeEnum.Customer;
                await _commentAppService.AddAsync(model);

                return RedirectToAction(nameof(Details)
                    , new
                    {
                        id = jobId,
                        internalMessage = "با موفقیت ایجاد شد"
                    });
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