using MeetUp.Data;
using MeetUp.Interfaces;
using MeetUp.Models;
using MeetUp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Configuration;
using System.Data;

namespace MeetUp.Controllers
{
    public class MeetActivitiesController : Controller
    {
        private readonly IMeetActivityService service;
        private readonly ICategoryService categoryService;
        private readonly ILocationService locationService;
        private readonly IUserActivityService userActivityService;

        public MeetActivitiesController(IMeetActivityService _service, ICategoryService _categoryService, ILocationService _locationService, IUserActivityService _userActivityService)
        {
            service = _service;
            categoryService = _categoryService;
            locationService = _locationService;
            userActivityService = _userActivityService;
        }

        public async Task<IActionResult> Index()
        {
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
            meetActivityViewModel.members = userActivityService.GetUsersByActivityId(meetActivity.Id).Result;
            meetActivityViewModel.canJoin = service.canJoin(meetActivity.Id, User.Identity.GetUserId(), User.Identity.IsAuthenticated);

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
            ViewData["LocationId"] = new SelectList(locationService.GetAll().Result, "Id", "Id");
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(MeetActivityCreateModel meetActivity)
        {
            var errors = service.Validate(meetActivity);
            if (errors.Length == 0)
            {
                service.Add(meetActivity, User.Identity.GetUserId());
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll().Result, "Id", "Id");
            ViewData["LocationId"] = new SelectList(locationService.GetAll().Result, "Id", "Id");
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
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll().Result, "Id", "Id", meetActivity.CategoryId);
            ViewData["LocationId"] = new SelectList(locationService.GetAll().Result, "Id", "Id", meetActivity.LocationId);
            return View(meetActivity);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Time,Capacity,Picture,LocationId,CategoryId")] MeetActivity meetActivity)
        {
            if (id != meetActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            { 
                service.Update(meetActivity);
            }


            ViewData["CategoryId"] = new SelectList(categoryService.GetAll().Result, "Id", "Id", meetActivity.CategoryId);
            ViewData["LocationId"] = new SelectList(locationService.GetAll().Result, "Id", "Id", meetActivity.LocationId);
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
