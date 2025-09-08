using Microsoft.EntityFrameworkCore;
using ToDoManagerAPI.Models;

namespace ToDoManagerAPI.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDoTask> Tasks { get; set; }
    }
}
