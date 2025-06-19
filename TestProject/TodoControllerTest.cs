using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo.Contracts;
using Todo.Controllers;
using Todo.Data;
using Todo.Models;
using Todo.Models.Dtos;
using Todo.Models.ResponseDtos;

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

    [Fact]
    public void AddTodoList()
    {
        var todoId = Guid.NewGuid();
        var mockService = new Mock<RetrieveAllTodos>();
        var mockedRequestedData = new AddTodoListDto
        {
            Title = "Test List",
        };
        
        mockService.Setup(s => s.AddTodoList(mockedRequestedData)).Returns(new MyTodoLists
        {
            Id = todoId,
            title = "Test List",
        });
        
        var controller = new TodoController(mockService.Object);
        var result = controller.AddTodoList(mockedRequestedData);
        var createdResult = Assert.IsType<CreatedResult>(result); 
        var returnValue = Assert.IsType<MyTodoLists>(createdResult.Value);
        
        Assert.Equal("Test List", returnValue.title);
        Assert.Equal(201,createdResult.StatusCode);
    }

    [Fact]
    public void AddTodoToList()
    {
       var todoId = Guid.NewGuid();
       var taskId = Guid.NewGuid();
       var mockService = new Mock<RetrieveAllTodos>();
       var mockedTodoAddRequest = new TodoAddRequest()
       {
           TodoListId = todoId,
           Title = "Test Task",
       };
       
       mockService.Setup(s => s.AddTodo(mockedTodoAddRequest)).Returns(new AddTodoResponse
       {
           Id = taskId,
           Title = mockedTodoAddRequest.Title,
           TodoListId = mockedTodoAddRequest.TodoListId,
           MyTodos = []
       });
       
       var controller = new TodoController(mockService.Object);
       var result = controller.AddTodoToList(mockedTodoAddRequest);
       
       var createdResult = Assert.IsType<CreatedResult>(result);
       var returnValue = Assert.IsType<AddTodoResponse>(createdResult.Value);
       
       Assert.Equal("Test Task", returnValue.Title);    
       Assert.Equal(201, createdResult.StatusCode);
       
    }
}