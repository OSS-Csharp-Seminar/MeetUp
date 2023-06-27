using MeetUp.Interfaces;
using MeetUp.Models;
using MeetUp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        // GET: MeetActivities
        public async Task<IActionResult> Index()
        {
            return View(await service.GetAll());
        }

        // GET: MeetActivities/Details/5
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
            meetActivityViewModel.members=userActivityService.GetUsersByActivityId(meetActivity.Id).Result;
            meetActivityViewModel.canJoin = service.canJoin(meetActivity.Id, User.Identity.GetUserId(), User.Identity.IsAuthenticated);
                
            if (meetActivity == null)
            {
                return NotFound();
            }
            return View(meetActivityViewModel);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll().Result, "Id", "Name");
            ViewData["LocationId"] = new SelectList(locationService.GetAll().Result, "Id", "Id");
            return View();
        }

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
            ViewData["CityId"] = new SelectList(locationService.GetAll().Result, "Id", "Name");
            ViewData["Errors"] = errors;
            return View(meetActivity);
        }

        // GET: MeetActivities/Edit/5
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

        // POST: MeetActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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

        // GET: MeetActivities/Delete/5
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

        // POST: MeetActivities/Delete/5
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
