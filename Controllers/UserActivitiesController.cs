using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetUp.Data;
using MeetUp.Models;
using MeetUp.Interfaces;
using Microsoft.AspNet.Identity;
using MeetUp.Services;

namespace MeetUp.Controllers
{
    public class UserActivitiesController : Controller
    {
        private readonly IUserActivityService service;
        private readonly IUserService userService;
        private readonly IMeetActivityService meetActivityService;
        private readonly IEmailService emailService;

        public UserActivitiesController(IUserActivityService _service, IUserService _userService, IMeetActivityService _meetActivityService, IEmailService _emailService)
        {
            service = _service;
            userService = _userService;
            meetActivityService = _meetActivityService;
            emailService = _emailService;
        }

        public async Task<IActionResult> Index()
        {
            var userActivities = await service.GetAll();
            return View(userActivities);
        }

        public async Task<IActionResult> Owned()
        {
            var userActivities = await service.GetByActivityOwner(User.Identity.GetUserId());
            return View(Approve);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userActivity = await service.GetById(id.Value);
            if (userActivity == null)
            {
                return NotFound();
            }

            return View(userActivity);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(string userId, int activityId)
        {
            service.Approve(userId, activityId);

            return RedirectToAction(nameof(Owned));
        }

        [HttpPost]
        public async Task<IActionResult> Create(int activityId)
        {
            service.Add(User.Identity.GetUserId(), activityId);
            var activity = meetActivityService.GetById(activityId).Result;
            EmailMessage message = new();
            message.GenerateNotifyAcitivtyOwner(activity.Owner.Email, activity);
            await emailService.SendEmailAsync(message);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userActivity = await service.GetById(id.Value);
            if (userActivity == null)
            {
                return NotFound();
            }
            ViewData["ActivityId"] = new SelectList(meetActivityService.GetAll().Result, "Id", "Id");
            ViewData["UserId"] = new SelectList(userService.GetAll().Result, "Id", "Id");
            return View(userActivity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ActivityId")] UserActivity userActivity)
        {
            if (id != userActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                    service.Update(userActivity);
             
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityId"] = new SelectList(meetActivityService.GetAll().Result, "Id", "Id");
            ViewData["UserId"] = new SelectList(userService.GetAll().Result, "Id", "Id");
            return View(userActivity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userActivity = await service.GetById(id.Value);
            if (userActivity == null)
            {
                return NotFound();
            }

            return View(userActivity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var userActivity = await service.GetById(id);
            if (userActivity != null)
            {
                service.Delete(userActivity);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
