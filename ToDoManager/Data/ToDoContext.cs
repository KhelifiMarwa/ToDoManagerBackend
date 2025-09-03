using Microsoft.EntityFrameworkCore;
using ToDoManager.Models;

namespace ToDoManager.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDoTask> Tasks { get; set; }
    }
}
