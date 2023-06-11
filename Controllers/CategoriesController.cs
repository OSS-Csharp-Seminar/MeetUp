using MeetUp.Interfaces;
using MeetUp.Models;
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

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await service.GetAll();
            return categories != null ?
                        View(categories) :
                        Problem("Entity set 'MeetUpContext.Category'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var category = await service.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                service.Add(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
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

        // GET: Categories/Delete/5
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

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
