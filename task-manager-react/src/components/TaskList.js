import { useState, useEffect } from "react";
import { getTasks } from "../services/taskService";
import { deleteTask } from "../services/taskService";
  



function TaskList(){
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);


  useEffect(() => {
    const loadTasks = async () => {
         setLoading(true);

        try {
            const res = await getTasks();
            setTasks(res);

        }catch (err) {
            setError(err);

        }finally {
            setLoading(false)
        }
    };

    loadTasks();
    
  }, []);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;


    return (
        <div>
            <h2>Tasks</h2>

            {tasks.length === 0 && !loading && (
                <p>No Tasks found</p>
            )}

            <ul>
                {tasks.map(task => (
                    <li key={task.id}>
                        <strong>{task.title}</strong>
                        <div>Status: {task.status}</div>
                        <div>Priority: {task.priority}</div>
                    </li>
                ))

                }
            </ul>

        </div>
    )
}

export default TaskList;