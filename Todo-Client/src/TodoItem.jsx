import { useContext } from "react";
import { TodoContext } from "./App";

export const TodoItem = ({ todoItem, todoId }) => {
  const { toggleTaskStatus,deleteTask } = useContext(TodoContext);
  const { done, id, todoTitle } = todoItem;

  console.log(deleteTask + " dleete task");
  
  return (
    <div className="todo-item">
      <div
        key={id}
        style={{ textDecoration: done ? "line-through" : "none" }}
        onClick={() => {
          return toggleTaskStatus(todoId, id);
        }}
      >
        {todoTitle}
      </div>
      <button onClick={()=>{
        return deleteTask(todoId,id)
      }}>Remove</button>
    </div>
  );
};
