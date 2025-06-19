using Microsoft.EntityFrameworkCore;
using Todo.Contracts;
using Todo.Data;
using Todo.Models.Dtos;
using Todo.Models.ResponseDtos;

namespace Todo.Models;

public class TodoService : RetrieveAllTodos
{
    private TodoDbContext dbContext;

    public TodoService(TodoDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<MyTodoLists> GetAllTodoLists()
    {
            var todos = dbContext.TodoLists.Include(t => t.Todos).ToList();
            return todos;
    }
    
    public MyTodoLists AddTodoList(AddTodoListDto todo)
    {
        var newTodo = new MyTodoLists
        {
            Id = Guid.NewGuid(),
            title = todo.Title,
        };
            
        dbContext.Add(newTodo);
        dbContext.SaveChanges();
     
        return newTodo;
    }
    
    
    public AddTodoResponse AddTodo(TodoAddRequest todo)
    {
        var requestedTodoList = dbContext.TodoLists.Find(todo.TodoListId);
            
        var newTodo = new MyTodos
        {
            Id = Guid.NewGuid(),
            TodoTitle = todo.Title,
            MyTodoListId = todo.TodoListId,
            MyTodoList = null,
            Done = false,
        };
            
        dbContext.Todos.Add(newTodo);
        dbContext.SaveChanges();
        
        var DtoTodo = new AddTodoResponse
        {
            Id = newTodo.Id,
            Title = newTodo.TodoTitle,
            TodoListId = newTodo.MyTodoListId,
            MyTodos = []
        };
        
        return DtoTodo;
    }
    
    public MyTodos ToggleStatus(Guid todoId)
    {
        var todo = dbContext.Todos.Find(todoId);
        todo.Done = !todo.Done;
        dbContext.Todos.Update(todo);
        dbContext.SaveChanges();

        return todo;
    }
    
    public MyTodos DeleteTodo(Guid id)
    {
        var todo = dbContext.Todos.Find(id);
        dbContext.Todos.Remove(todo);
        dbContext.SaveChanges();

        return todo;
    }
    
    public MyTodoLists DeleteTodoList(Guid todoListId)
    {
        var todos = dbContext.Todos
            .Where(todo => todo.MyTodoListId == todoListId)
            .ToList();
        
        dbContext.Todos.RemoveRange(todos);
        
        var todoList = dbContext.TodoLists.Find(todoListId);
        if (todoList != null)
        {
            dbContext.TodoLists.Remove(todoList);
        }
        
        dbContext.SaveChanges();
        return todoList;
    }
}