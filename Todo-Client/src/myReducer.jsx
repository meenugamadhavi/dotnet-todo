const getAllTodos = (data) => {
  console.log(data+" data from the server");
  
  return data;
};

const addTodos = (state, title) => {
  fetch("/api/todos/add-todo-list", {
    method: "POST",
    headers:{
      "Content-Type": "application/json",
    },
    body: JSON.stringify({Title:title}),
  }).then((response) => response.json());

  return state;
};


const addTask = (state, task, todoId) => {
  fetch("/api/todos/add-todo", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ Title:task,TodoListId:todoId }),
  }).then((response) => response.json());

  return state;
};

const toggleTaskStatus = (state, todoId, taskId) => {
  todoId = taskId
  fetch(`/api/todos/${todoId}`, {
    method: "PATCH",
  }).then((response) => response.json());

  return state;
};

const deleteTodo = (state, todoId) => {
  const todoListId = todoId;
  fetch(`/api/todos/delete-todo-list/${todoListId}`, {
    method: "DELETE",
  }).then((response) => response.json());

  return state;
};

const deleteTask = (state,todoId,taskId) => {
   const id = taskId;
  fetch(`/api/todos/delete-todo/${id}`, {
    method: "DELETE",
  }).then((response) => response.json());
  
  return state;
}

export const myReducer = (state, action) => {
  const actions = {
    "all-todos": () => getAllTodos(action.data),
    "add-todo": () => addTodos(state, action.title),
    "add-task": () => addTask(state, action.task, action.todoId),
    "toggle-status": () =>
      toggleTaskStatus(state, action.todoId, action.taskId),
    "delete-todo": () => deleteTodo(state, action.todoId),
    "delete-task" : () => deleteTask(state, action.todoId,action.taskId)
  };

  return actions[action.type]();
};
