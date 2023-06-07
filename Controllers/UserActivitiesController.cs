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
    public class UserActivitiesController : Controller
    {
        private readonly MeetUpContext _context;

        public UserActivitiesController(MeetUpContext context)
        {
            _context = context;
        }

        // GET: UserActivities
        public async Task<IActionResult> Index()
        {
            var meetUpContext = _context.UserActivity.Include(u => u.Activity).Include(u => u.User);
            return View(await meetUpContext.ToListAsync());
        }

        // GET: UserActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserActivity == null)
            {
                return NotFound();
            }

            var userActivity = await _context.UserActivity
                .Include(u => u.Activity)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userActivity == null)
            {
                return NotFound();
            }

            return View(userActivity);
        }

        // GET: UserActivities/Create
        public IActionResult Create()
        {
            ViewData["ActivityId"] = new SelectList(_context.MeetActivity, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
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
                _context.Add(userActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityId"] = new SelectList(_context.MeetActivity, "Id", "Id", userActivity.ActivityId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userActivity.UserId);
            return View(userActivity);
        }

        // GET: UserActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserActivity == null)
            {
                return NotFound();
            }

            var userActivity = await _context.UserActivity.FindAsync(id);
            if (userActivity == null)
            {
                return NotFound();
            }
            ViewData["ActivityId"] = new SelectList(_context.MeetActivity, "Id", "Id", userActivity.ActivityId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userActivity.UserId);
            return View(userActivity);
        }

        // POST: UserActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                try
                {
                    _context.Update(userActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserActivityExists(userActivity.Id))
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
            ViewData["ActivityId"] = new SelectList(_context.MeetActivity, "Id", "Id", userActivity.ActivityId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userActivity.UserId);
            return View(userActivity);
        }

        // GET: UserActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserActivity == null)
            {
                return NotFound();
            }

            var userActivity = await _context.UserActivity
                .Include(u => u.Activity)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.UserActivity == null)
            {
                return Problem("Entity set 'MeetUpContext.UserActivity'  is null.");
            }
            var userActivity = await _context.UserActivity.FindAsync(id);
            if (userActivity != null)
            {
                _context.UserActivity.Remove(userActivity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserActivityExists(int id)
        {
          return (_context.UserActivity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
