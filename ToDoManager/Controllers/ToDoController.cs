using Microsoft.AspNetCore.Mvc;
using ToDoManager.Models;
using ToDoManager.Services;

namespace ToDoManager.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoService _service;

        // Injection de dépendance
        public ToDoController(ToDoService service)
        {
            _service = service;
        }

        // GET: /ToDo
        public async Task<IActionResult> Index()
        {
            var tasks = await _service.GetAllAsync();
            return View(tasks ?? new List<ToDoTask>());
        }

        // GET: /ToDo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // GET: /ToDo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ToDo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoTask task)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: /ToDo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: /ToDo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToDoTask task)
        {
            if (id != task.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: /ToDo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: /ToDo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
