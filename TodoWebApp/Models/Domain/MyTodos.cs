namespace Todo.Models
{ 
    public class MyTodos
    {
        public Guid Id { get; set; }
        public string TodoTitle { get; set; }
        public bool Done { get; set; }
        
        public Guid MyTodoListId { get; set; }
        public MyTodoLists MyTodoList { get; set; }
    }
}