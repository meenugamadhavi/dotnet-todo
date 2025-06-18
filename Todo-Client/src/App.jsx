import { useEffect, useReducer, createContext } from "react";
import "/src/App.css";
import { myReducer } from "./myReducer";
import { Input } from "./Input";
import { Todo } from "./Todo";

export const TodoContext = createContext(null);

const Todos = () => {
  const [todos, dispatch] = useReducer(myReducer, []);

  const fetchData = () => {
    fetch("/api/todos")
      .then((response) => response.json())
      .then((data) => dispatch({ type: "all-todos", data }));
  };

  useEffect(() => {
    setInterval(() => {
      fetchData();
    }, 300);
  }, []);

  const addTodo = (title) => {
    dispatch({ type: "add-todo", title });
  };

  const addTask = (task, todoId) => {
    dispatch({ type: "add-task", task, todoId });
  };

  const toggleTaskStatus = (todoId, taskId) => {
    dispatch({ type: "toggle-status", todoId, taskId });
  };

  const deleteTodo = (todoId) => {
    dispatch({ type: "delete-todo", todoId });
  };

  const deleteTask = (todoId,taskId) => {
    dispatch({type:"delete-task",todoId,taskId})
  }

  //  {
  //   "title": "Buy Milk",
  //   "id": "992307bb-fc14-4aef-8b88-27137366492c",
  //   "todos": [
  //     {
  //       "id": "98ba5fc2-8928-4210-a05e-e5a0a777bf79",
  //       "todoTitle": "task1",
  //       "done": false,
  //       "myTodoListId": "992307bb-fc14-4aef-8b88-27137366492c",
  //       "myTodoList": null
  //     },
  //     {
  //       "id": "15ec72b3-6a60-4ec8-aa5d-803630a7c42a",
  //       "todoTitle": "task2",
  //       "done": false,
  //       "myTodoListId": "992307bb-fc14-4aef-8b88-27137366492c",
  //       "myTodoList": null
  //     }
  //   ]
  // }

  return (
    <TodoContext.Provider
      value={{
        addItem: addTodo,
        addTask: addTask,
        toggleTaskStatus: toggleTaskStatus,
        deleteTodo: deleteTodo,
        deleteTask: deleteTask
      }}
    >
      <div className="header">
      <h1>Add Todo Lists</h1>
      <Input/>
      </div>
      <div>
        {todos.map((todo, index) => {
          return <Todo todoItem={todo} key={index+1} />;
        })}
      </div>
    </TodoContext.Provider>
  );
};

export default Todos;
