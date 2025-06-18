using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.Models.Dtos;
using Todo.Models.ResponseDtos;

namespace Todo.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController(TodoDBContext todosObj) : ControllerBase
    {
        private readonly TodoDBContext dbContext = todosObj;

        [HttpGet]
        public IActionResult AllTodoLists()
        {
            var todos = dbContext.TodoLists.Include(t => t.Todos).ToList();
            return Ok(todos);
        }

        [HttpPost("add-todo-list")]
        public IActionResult AddTodoList([FromBody] AddTodoListDto todo)
        {
            var newTodo = new MyTodoLists
            {
                Id = Guid.NewGuid(),
                title = todo.Title,
            };
            Console.WriteLine("-------------------------------------------");
            dbContext.Add(newTodo);
            dbContext.SaveChanges();

            return CreatedAtAction("AddTodoList", newTodo, null);
        }

        [HttpPost("add-todo")]
        public IActionResult AddTodo([FromBody] TodoAddRequest todo)
        {
            var requestedTodoList = dbContext.TodoLists.Find(todo.TodoListId);
           
            
            if (requestedTodoList == null)
            {
                return NotFound("TodoList not found");
            }
            
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

            return CreatedAtAction("AddTodoList", DtoTodo, null);
        }

        [HttpPatch("{todoId:guid}")]
        public IActionResult ToggleStatus(Guid todoId)
        {
            var todo = dbContext.Todos.Find(todoId);
            todo.Done = !todo.Done;
            dbContext.Todos.Update(todo);
            dbContext.SaveChanges();

            return Ok(todo);
        }

        [HttpDelete("delete-todo/{id:guid}")]
        public IActionResult DeleteTodo(Guid id)
        {
            var todo = dbContext.Todos.Find(id);
            dbContext.Todos.Remove(todo);
            dbContext.SaveChanges();

            return Ok(todo);
        }

        [HttpDelete("delete-todo-list/{todoListId:guid}")]
        public IActionResult DeleteTodoList(Guid todoListId)
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
            return Ok(todoList);
        }
    }
}