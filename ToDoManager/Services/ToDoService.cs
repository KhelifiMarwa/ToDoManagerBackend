using Microsoft.EntityFrameworkCore;
using ToDoManager.Data;
using ToDoManager.Models;

namespace ToDoManager.Services
{
    
        public class ToDoService
        {
            private readonly ToDoContext _context;

            public ToDoService(ToDoContext context)
            {
                _context = context;
            }

            public async Task<List<ToDoTask>> GetAllAsync()
            {
                return await _context.Tasks.ToListAsync();
            }

            public async Task<ToDoTask?> GetByIdAsync(int id)
            {
                return await _context.Tasks.FindAsync(id);
            }

            public async Task CreateAsync(ToDoTask task)
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(ToDoTask task)
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task != null)
                {
                    _context.Tasks.Remove(task);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
    
