import { useEffect, useState } from "react";
import { getTasks, completeTask, deleteTask } from "../api/tasks.api";
import type { Task } from "../types/task.types";
import CreateTaskForm from "../components/CreateTaskForm";


const TasksPage = () => {
    const [tasks, setTasks] = useState<Task[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    const fetchTasks = async () => {
        try {
            const response = await getTasks();
            setTasks(response.items);
        } catch {
            setError(
                "Failed to load tasks"
            );
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchTasks();
    }, []);
      
    const handleDeleteTask = async (taskId: string) => {
        try {
            await deleteTask(taskId);
            fetchTasks();
        } catch {
            alert("Failed to delete task");
        }
    };

    const handleCompleteTask = async (taskId: string) => {
        try {
            await completeTask(taskId);
            fetchTasks();
        } catch {
            alert("Failed to complete task");
        }
    };
    
    if (loading) {
        return <p>Loading...</p>;
    }
    if (error) {
        return <p>{error}</p>;
    }

    return (
        <div>
        <CreateTaskForm onTaskCreated={fetchTasks} />
            <h2>Tasks</h2>
            {tasks.map(
                (task) => (
                    <div key={task.id}>
                        <h3>{task.title}</h3>
                        <p>{task.description}</p>
                        <p>Priority: {task.priority}</p>
                        <p>Completed: {task.isCompleted ? "Yes" : "No"}</p>
                        
                        {!task.isCompleted && (
                            <button onClick={() => handleCompleteTask(task.id)}>
                            Complete
                            </button>
                        )}
                        <br />
                        
                        <button onClick={() => handleDeleteTask(task.id)}>
                            Delete
                        </button>

                    </div>
                )
            )}
        </div>
    );
    
};

export default TasksPage;