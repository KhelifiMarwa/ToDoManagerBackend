using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoManagerAPI.Data;
using ToDoManagerAPI.Models;
using ToDoManagerAPI.Services;

namespace ToDoManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService _service;

        public ToDoController(ToDoService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoTask>>> GetAll()
        {
            var tasks = await _service.GetAllAsync();
            return Ok(tasks);
        }

        // GET: api/todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoTask>> GetById(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<ToDoTask>> Create(ToDoTask task)
        {
            if (task == null) return BadRequest();

            var created = await _service.CreateAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ToDoTask task)
        {
            if (id != task.Id) return BadRequest();

            var updated = await _service.UpdateAsync(task);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }

}
