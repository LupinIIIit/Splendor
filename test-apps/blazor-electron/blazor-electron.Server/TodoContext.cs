using Microsoft.EntityFrameworkCore;
using blazor_electron.Models;

namespace blazor_electron.Server {
    public class TodoContext : DbContext {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
        public DbSet<TodoItem> Todos { get; set; }

    }
}
