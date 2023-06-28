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
using MeetUp.ViewModels;

namespace MeetUp.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IRatingService service;
        private readonly IUserService userService;

        public RatingsController(IRatingService _service, IUserService _userService)
        {
            service = _service;
            userService = _userService;
        }

        public async Task<IActionResult> Index()
        {
            var ratings = await service.GetAll();
            return View(ratings);
        }

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

        public IActionResult Create()
        {
            ViewData["RevieweeId"] = new SelectList(userService.GetAll().Result, "Id", "UserName");
            return View();
        }
        [HttpGet]
        public IActionResult AddRating(string id)
        {
         
            AddRatingViewModel rating = new AddRatingViewModel
            {
                Id = id,
            };
           
            return View(rating);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRatingPost([Bind("Score,Message,Id")] AddRatingViewModel newRating)
        {
         
            if (ModelState.IsValid)
            {
                var rating = new Rating
                {
                    Score = newRating.Score.Value,
                    Message = newRating.Message,
                    RevieweeId = newRating.Id

                };
               
                var result = service.Add(rating);
                return RedirectToAction("Users", "Details", new { id = ViewData["userId"] });
            }
            
            return RedirectToAction("Users", "Details",new{id= ViewData["userId"] });
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
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
