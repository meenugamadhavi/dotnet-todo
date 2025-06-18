import { useContext } from "react";
import { TodoContext } from "./App";
import { TodoItem } from "./TodoItem";

export const TodoItems = () => {
  const { todoItems, todoId } = useContext(TodoContext);
console.log("*".repeat(30));
console.log("Todo Id " + todoId);

  return (
    <div className="todo-items">
      {todoItems.map((todoItem) => {
        return <TodoItem todoItem={todoItem} todoId={todoId} key={todoId} />;
      })}
    </div>
  );
};

