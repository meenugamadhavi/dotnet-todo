using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) :base(options)
        {
        }
        
        public DbSet<MyTodos> Todos { get; set; }
        public DbSet<MyTodoLists> TodoLists { get; set; }
    }
}