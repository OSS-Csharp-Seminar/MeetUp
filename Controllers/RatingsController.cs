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
using Microsoft.AspNetCore.Authorization;

namespace MeetUp.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IRatingService service;
        private readonly IUserService userService;

        public RatingsController(IRatingService _service, IUserService _userService)
        {
            service =_service;
            userService = _userService;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            var ratings = await service.GetAll();
            return View(ratings);
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = service.GetById(id.Value);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["RevieweeId"] = new SelectList(userService.GetAll().Result, "Id", "UserName");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Score,Message,RevieweeId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                var result = service.Add(rating);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RevieweeId"] = new SelectList(userService.GetAll().Result, "Id", "UserName", rating.RevieweeId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await service.GetById(id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["RevieweeId"] = new SelectList(userService.GetAll().Result, "Id", "UserName", rating.RevieweeId);
            return View(rating);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Score,Message,RevieweeId")] Rating rating)
        {
            if (id != rating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                service.Update(rating);
            }
      
            ViewData["RevieweeId"] = new SelectList(userService.GetAll().Result, "Id", "Id", rating.RevieweeId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var rating = await service.GetById(id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var rating = await service.GetById(id);
            if (rating != null)
            {
                service.Delete(rating);
            }
   
            return RedirectToAction(nameof(Index));
        }

   
    }
}
