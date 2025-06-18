namespace Todo.Models.ResponseDtos;

public class AddTodoResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid TodoListId { get; set; }
    public List<MyTodos> MyTodos { get; set; }
}