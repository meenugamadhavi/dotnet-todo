using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
    public class TodoDBContext : DbContext
    {
        public TodoDBContext(DbContextOptions<TodoDBContext> options) :base(options)
        {
        }
        
        public DbSet<MyTodos> Todos { get; set; }
        public DbSet<MyTodoLists> TodoLists { get; set; }
    }
}