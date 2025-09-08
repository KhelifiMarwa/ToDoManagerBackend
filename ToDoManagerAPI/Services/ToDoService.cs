using Microsoft.EntityFrameworkCore;
using ToDoManagerAPI.Data;
using ToDoManagerAPI.Models;

namespace ToDoManagerAPI.Services
{

    public class ToDoService
    {
        private readonly ToDoContext _context;

        public ToDoService(ToDoContext context)
        {
            _context = context;
        }

        // GET all tasks
        public async Task<List<ToDoTask>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET task by id
        public async Task<ToDoTask?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        // CREATE new task
        public async Task<ToDoTask> CreateAsync(ToDoTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        // UPDATE task
        public async Task<bool> UpdateAsync(ToDoTask task)
        {
            if (!await _context.Tasks.AnyAsync(t => t.Id == task.Id))
                return false;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE task
        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
    
