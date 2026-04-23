import { useState, useEffect } from "react";
import { getTasks } from "../services/taskService";
import { deleteTask } from "../services/taskService";

function TaskList() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const loadTasks = async () => {
      setLoading(true);

      try {
        const res = await getTasks();
        setTasks(res);
      } catch (err) {
        setError(err);
      } finally {
        setLoading(false);
      }
    };

    loadTasks();
  }, []);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error.message}</div>;

  const handleDelete = async (id) => {
    const confirmed = window.confirm(
      "Are you sure you want to delete this task?"
    );
    if (!confirmed) return;

    try {
      await deleteTask(id);
      setTasks((prev) => prev.filter((t) => t.id !== id));
    } catch (err) {
      alert("failed to delete task");
    }
  };

  return (
    <div>
      <h2>Tasks</h2>

      {tasks.length === 0 && !loading && !error && <p>No Tasks found</p>}

      <ul>
        {tasks.map((task) => (
          <li key={task.id}>
            <strong>{task.title}</strong>
            <div>Status: {task.status}</div>
            <div>Priority: {task.priority}</div>
            <button onClick={() => handleDelete(task.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default TaskList;
