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
using Microsoft.AspNetCore.Authorization;

namespace MeetUp.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILocationService service;

        public LocationsController(ILocationService _service)
        {
            service = _service;
        }
        
        public async Task<IActionResult> Index()
        {
            var locations = await service.GetAll();
            return locations != null ? 
                          View(locations) :
                          Problem("Entity set 'MeetUpContext.Location'  is null.");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await service.GetById(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Longitude,Latitude,City")] Location location)
        {
            if (ModelState.IsValid)
            {
                service.Add(location);
                
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var location = await service.GetById(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Longitude,Latitude,City")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                   service.Update(location);

            }
            return View(location);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var location = await service.GetById(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            var location = await service.GetById(id);
            if (location != null)
            {
                service.Delete(location);
            }
            
            return RedirectToAction(nameof(Index));
        }

  
    }
}
