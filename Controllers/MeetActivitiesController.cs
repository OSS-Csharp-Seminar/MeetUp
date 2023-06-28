using MeetUp.Interfaces;
using MeetUp.Models;
using MeetUp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeetUp.Controllers
{
    public class MeetActivitiesController : Controller
    {
        private readonly IMeetActivityService service;
        private readonly ICategoryService categoryService;
        private readonly ICityService cityService;
        private readonly IUserActivityService userActivityService;

        public MeetActivitiesController(IMeetActivityService _service, ICategoryService _categoryService, ICityService _cityService, IUserActivityService _userActivityService)
        {
            service = _service;
            categoryService = _categoryService;
            cityService = _cityService;
            userActivityService = _userActivityService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
               return View(await service.GetAllByCityName(searchString));
            }
            return View(await service.GetAll());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // TODO: Move to service layer GetViewModel
            var meetActivity = service.GetById(id.Value).Result;
            var meetActivityViewModel = new MeetActivityViewModel();
            meetActivityViewModel.meetActivity = meetActivity;
            meetActivityViewModel.members = userActivityService.ApprovedUsers(meetActivity.Id).Result;
            meetActivityViewModel.canJoin = service.canJoin(meetActivity.Id, User.Identity.GetUserId(), User.Identity.IsAuthenticated);
            meetActivityViewModel.canEdit = service.canEdit(meetActivity.Id, User.Identity.GetUserId());

            if (meetActivity == null)
            {
                return NotFound();
            }
            return View(meetActivityViewModel);
        }
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll().Result, "Id", "Name");
            ViewData["CityId"] = new SelectList(cityService.GetAll().Result, "Id", "Name");
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(MeetActivityCreateModel meetActivity)
        {
            var errors = service.ValidateDate(meetActivity.Time);
            if (errors.Length == 0)
            {
                service.Add(meetActivity, User.Identity.GetUserId());
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll().Result, "Id", "Name");
            ViewData["CityId"] = new SelectList(cityService.GetAll().Result, "Id", "Name");
            ViewData["Errors"] = errors;
            return View(meetActivity);
        }
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetActivity = service.GetById(id.Value).Result;
            if (meetActivity == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll().Result, "Id", "Name", meetActivity.CategoryId);
            ViewData["CityId"] = new SelectList(cityService.GetAll().Result, "Id", "Name", meetActivity.LocationId);
            return View(new MeetActivityEditModel(meetActivity));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, MeetActivityEditModel meetActivity)
        {
            if (id != meetActivity.Id)
            {
                return NotFound();
            }
            var errors = service.ValidateDate(meetActivity.Time);
            if (errors.Length == 0)
            {
                service.Update(meetActivity);
                return RedirectToAction(nameof(Details), new {id = id});
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll().Result, "Id", "Name");
            ViewData["CityId"] = new SelectList(cityService.GetAll().Result, "Id", "Name");
            ViewData["Errors"] = errors;
            return View(meetActivity);
        }

        // [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetActivity = service.GetById(id.Value).Result;
            if (meetActivity == null)
            {
                return NotFound();
            }

            return View(meetActivity);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var meetActivity = await service.GetById(id);
            if (meetActivity != null)
            {
                service.Delete(meetActivity);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
