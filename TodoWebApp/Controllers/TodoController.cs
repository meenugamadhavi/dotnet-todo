using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Contracts;
using Todo.Data;
using Todo.Models;
using Todo.Models.Dtos;
using Todo.Models.ResponseDtos;

namespace Todo.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController(RetrieveAllTodos todoService) : ControllerBase
    {
        private readonly RetrieveAllTodos todoService = todoService;

        [HttpGet]
        public IActionResult AllTodoLists()
        {
            var todos = todoService.GetAllTodoLists();
            return Ok(todos);
        }

        [HttpPost("add-todo-list")]
        public IActionResult AddTodoList([FromBody] AddTodoListDto todo)
        {
            var newTodo = todoService.AddTodoList(todo);

            return Created("AddTodoList", newTodo);
        }

        [HttpPost("add-todo")]
        public IActionResult AddTodo([FromBody] TodoAddRequest todo)
        {
            var DtoTodo = todoService.AddTodo(todo);

            return CreatedAtAction("AddTodoList", DtoTodo, null);
        }
        
        [HttpPatch("{todoId:guid}")]
        public IActionResult ToggleStatus(Guid todoId)
        {
            var todo = todoService.ToggleStatus(todoId);

            return Ok(todo);
        }

        [HttpDelete("delete-todo/{id:guid}")]
        public IActionResult DeleteTodo(Guid id)
        {
            try
            {
                var todo = todoService.DeleteTodo(id);

                return Ok(todo);
            }
            catch (Exception ex)
            {
                return NotFound($"{id} is not found");
            }
        }

        [HttpDelete("delete-todo-list/{todoListId:guid}")]
        public IActionResult DeleteTodoList(Guid todoListId)
        {
            try
            {
                var todos = todoService.DeleteTodoList(todoListId);
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return NotFound($"{todoListId} is not found");
            }
        }
    }
}