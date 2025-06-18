import { useContext } from "react";
import { TodoContext } from "./App";
import { TodoItems } from "./TodoItems";
import { TodoTitle } from "./TodoTitle";
import { Input } from "./Input";

export const Todo = ({ todoItem }) => {
  const { addTask, toggleTaskStatus, deleteTodo,deleteTask } = useContext(TodoContext);

  const addTodoItem = (task) => {
    addTask(task, todoItem.id);
  };

  //changing tasks to todos
  const context = {
    addItem: addTodoItem,
    toggleTaskStatus,
    todoItems: todoItem.todos,
    todoId: todoItem.id,
    deleteTask
  };

  return (
    <TodoContext.Provider value={context}>
      <div>
        <TodoTitle title={todoItem.title} />
        <div className="task-input">
          <Input />
          <button onClick={() => deleteTodo(todoItem.id)}>Delete</button>
        </div>
        <TodoItems />
      </div>
    </TodoContext.Provider>
  );
};
