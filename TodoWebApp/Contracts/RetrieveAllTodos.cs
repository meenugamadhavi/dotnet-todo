using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Models.Dtos;
using Todo.Models.ResponseDtos;

namespace Todo.Contracts
{

    public interface RetrieveAllTodos
    {
        List<MyTodoLists> GetAllTodoLists();
        MyTodoLists AddTodoList(AddTodoListDto todo);
        AddTodoResponse AddTodo(TodoAddRequest todo);
        MyTodos ToggleStatus(Guid todoId);
        MyTodos DeleteTodo(Guid id);
        MyTodoLists DeleteTodoList(Guid todoListId);
    }
}