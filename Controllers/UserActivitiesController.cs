﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetUp.Data;
using MeetUp.Models;
using MeetUp.Interfaces;

namespace MeetUp.Controllers
{
    public class UserActivitiesController : Controller
    {
        private readonly IUserActivityService service;
        private readonly IUserService userService;
        private readonly IMeetActivityService meetActivityService;

        public UserActivitiesController(IUserActivityService _service, IUserService _userService, IMeetActivityService _meetActivityService)
        {
            service = _service;
            userService = _userService;
            meetActivityService = _meetActivityService;
        }

        // GET: UserActivities
        public async Task<IActionResult> Index()
        {
            var userActivities = service.GetAll();
            return View(await userActivities);
        }

        // GET: UserActivities/Details/5
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

        // GET: UserActivities/Create
        public IActionResult Create()
        {
            ViewData["ActivityId"] = new SelectList(meetActivityService.GetAll().Result, "Id", "Id");
            ViewData["UserId"] = new SelectList(userService.GetAll().Result, "Id", "Id");
            return View();
        }

        // POST: UserActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ActivityId")] UserActivity userActivity)
        {
            if (ModelState.IsValid)
            {
                service.Add(userActivity);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityId"] = new SelectList(meetActivityService.GetAll().Result, "Id", "Id");
            ViewData["UserId"] = new SelectList(userService.GetAll().Result, "Id", "Id");
            return View(userActivity);
        }

        // GET: UserActivities/Edit/5
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

        // GET: UserActivities/Delete/5
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

        // POST: UserActivities/Delete/5
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
