using MeetUp.Data;
using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace MeetUp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService service;

        public CategoriesController(ICategoryService _service)
        {
            service = _service;
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Index()
        {
            var categories = await service.GetAll();
            return categories != null ?
                        View(categories) :
                        Problem("Entity set 'MeetUpContext.Category'  is null.");
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Details(int id)
        {

            var category = await service.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                service.Add(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {

            var category = await service.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {

            if (ModelState.IsValid)
            {

                var updated = service.Update(category);
                if (!updated)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
  
            var category = await service.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var deleted = service.Delete(category);


            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = service.GetById(id);
            if (category != null)
            {
                service.Delete(category.Result);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
