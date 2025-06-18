namespace Todo.Models.Dtos;

public class TodoAddRequest
{
    public Guid TodoListId { get; set; }
    public string Title { get; set; }
}