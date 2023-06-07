using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetUp.Data;
using MeetUp.Models;

namespace MeetUp.Controllers
{
    public class MeetActivitiesController : Controller
    {
        private readonly MeetUpContext _context;

        public MeetActivitiesController(MeetUpContext context)
        {
            _context = context;
        }

        // GET: MeetActivities
        public async Task<IActionResult> Index()
        {
            var meetUpContext = _context.MeetActivity.Include(m => m.Category).Include(m => m.Location);
            return View(await meetUpContext.ToListAsync());
        }

        // GET: MeetActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MeetActivity == null)
            {
                return NotFound();
            }

            var meetActivity = await _context.MeetActivity
                .Include(m => m.Category)
                .Include(m => m.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetActivity == null)
            {
                return NotFound();
            }

            return View(meetActivity);
        }

        // GET: MeetActivities/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id");
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "Id");
            return View();
        }

        // POST: MeetActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Time,Capacity,Picture,LocationId,CategoryId")] MeetActivity meetActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meetActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = new SelectList(_context.Category, "Name", "Name", meetActivity.CategoryId);
            ViewData["Location"] = new SelectList(_context.Location, "Name", "Name", meetActivity.LocationId);
            return View(meetActivity);
        }

        // GET: MeetActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MeetActivity == null)
            {
                return NotFound();
            }

            var meetActivity = await _context.MeetActivity.FindAsync(id);
            if (meetActivity == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", meetActivity.CategoryId);
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "Id", meetActivity.LocationId);
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
                try
                {
                    _context.Update(meetActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetActivityExists(meetActivity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", meetActivity.CategoryId);
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "Id", meetActivity.LocationId);
            return View(meetActivity);
        }

        // GET: MeetActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeetActivity == null)
            {
                return NotFound();
            }

            var meetActivity = await _context.MeetActivity
                .Include(m => m.Category)
                .Include(m => m.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.MeetActivity == null)
            {
                return Problem("Entity set 'MeetUpContext.MeetActivity'  is null.");
            }
            var meetActivity = await _context.MeetActivity.FindAsync(id);
            if (meetActivity != null)
            {
                _context.MeetActivity.Remove(meetActivity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetActivityExists(int id)
        {
          return (_context.MeetActivity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
