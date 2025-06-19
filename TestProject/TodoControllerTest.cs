using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo.Contracts;
using Todo.Controllers;
using Todo.Data;
using Todo.Models;

namespace TestProject;

public class TodoControllerTest
{
    [Fact]
    public void GetAllTodos()
    {
        var todoId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var fakeData = new List<MyTodoLists>
        {
            new MyTodoLists
            {
                Id = todoId,
                title = "Test List",
                Todos = new List<MyTodos>
                {
                    new MyTodos
                    {
                        Id = taskId,
                        Done = false,
                        TodoTitle = "Buy Car",
                        MyTodoListId = todoId
                    }
                }
            }
        };

        var mockService = new Mock<RetrieveAllTodos>();
        mockService.Setup(s => s.GetAllTodoLists()).Returns(fakeData);

        var controller = new TodoController(mockService.Object);

        var result = controller.AllTodoLists();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<MyTodoLists>>(okResult.Value);
        Assert.Single(returnValue);
        Assert.Equal(fakeData,returnValue);
      }
}