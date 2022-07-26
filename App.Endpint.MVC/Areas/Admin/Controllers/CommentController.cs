using App.AppServices;
using App.Domain.Contracts.AppService;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentAppService _commentAppService;
        private readonly IJobAppService _jobAppService;

        public CommentController(ICommentAppService commentAppService, IJobAppService jobAppService)
        {
            _commentAppService = commentAppService;
            _jobAppService = jobAppService;
        }

        public async Task<ActionResult> Index(string? internalMessage = null)
        {
            if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);

            var model = await _commentAppService.GetAllAsync();

            return View(model);
        }

        public async Task<ActionResult> Details(int id, string? internalMessage = null)
        {
            if (internalMessage != null) ModelState.AddModelError("internalMessage", internalMessage);
            var model = await _commentAppService.GetByIdAsync(id);
            ViewData["Job"] = await _jobAppService.GetByIdAsync(model.JobId);
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _commentAppService.DeleteAsync(id);
                return RedirectToAction(nameof(Index)
                    , new { internalMessage = "با موفقیت حذف شد" });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index)
                    , new { internalMessage = "خطا : " + e.Message });
            }
        }

        public async Task<ActionResult> ConfirmComment(int id)
        {
            try
            {
                await _commentAppService.ConfirmAsync(id);
                return RedirectToAction(nameof(Details), new { id = id, internalMessage = "با موفقیت تایید شد" });
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Details), new { id = id, internalMessage = "خطا ! در فرآیند تایید مشکلی به وجود آمد" });
            }
        }
    }
}
