import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7039/api",
  headers: {
    "Content-Type": "application/json"
  },
  timeout: 10000 // 10 seconds (optional)
});

export async function getTasks(){
    const response  = await api.get('/tasks')
    return response.data

}

export async function getTasksById(id){

    const response = await api.get(`/tasks/${id}`)
    return response.data

}

export async function createTask(task){
    const response = await api.post("/tasks", task)
    return response.data;

}

export async function updateTask(id, task){
    const response = await api.put(`/tasks/${id}`, task )
    return response.data;

}

export async function deleteTask(id){
    const response = await api.delete(`/tasks/${id}` )
    return response.data;

}

export default api;