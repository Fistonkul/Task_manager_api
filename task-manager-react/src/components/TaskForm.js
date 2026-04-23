import { useState } from "react";
import { Form, Button } from "react-router-dom";
import { createTask } from "../services/taskService";

function TaskForm() {
  const [task, setTask] = useState({
    title: "",
    priority: "Medium",
    status: "Not Started",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setTask((prev) => ({
      ...prev,
      [name]: value,
    }));
  };
  const handleSubmit = (e) =>{
    e.preventDefault();

    const message = "error";

  }

  return (
    <Form.Group>
      <Form.Control
        type="text"
        name="title"
        placeholder="title"
        value={task.title}
        onChange={handleChange}
      />

      <Form.Control
        type="text"
        name="priority"
        placeholder="priority"
        value={task.priority}
        onChange={handleChange}
      />

      <Form.Control
        type="text"
        name="status"
        placeholder="status"
        value={task.status}
        onChange={handleChange}
      />

      <Button type="submit">Submit</Button>

    </Form.Group>
  );
}

export default TaskForm;
