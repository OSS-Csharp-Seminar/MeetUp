using MeetUp.Data;
using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MeetUp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService service;

        public UsersController(IUserService _service)
        {
            service = _service;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await service.GetAll();
            return users != null ?
                        View(users) :
                        Problem("Entity set 'MeetUpContext.User'  is null.");
        }
        
        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await service.GetUserDetails(id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }
        [Authorize(Roles = UserRoles.Admin)]
        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Password")] AppUser user)
        { 
            if (ModelState.IsValid)
            {
                //user.Role = Role.USER;
                service.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await service.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Password,Image")] AppUser user)
        {


            if (ModelState.IsValid)
            {

                service.Update(user);

            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await service.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await service.GetById(id);
            if (user != null)
            {
                service.Delete(user);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
