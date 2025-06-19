namespace Todo.Models;

public class MyTodoLists
{
    public string title { get; set; }
    public Guid Id { get; set; }
    public List<MyTodos> Todos { get; set; } = new();
}