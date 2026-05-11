import { useEffect, useState } from "react";
import { getTasks, completeTask, deleteTask } from "../api/tasks.api";
import type { Task } from "../types/task.types";
import CreateTaskForm from "../components/CreateTaskForm";
import { logout } from "../../auth/utils/logout";


const TasksPage = () => {
    const [tasks, setTasks] = useState<Task[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [page, setPage] = useState(1);
    const pageSize = 3;
    const [totalPages, setTotalPages] = useState(1);
    const [priority, setPriority] = useState<number | null>(null);

    const fetchTasks = async () => {
        setLoading(true);

        try {
            const response = await getTasks(page, pageSize, priority);
            setTasks(response.items);
            setTotalPages(
                Math.ceil(response.totalCount / pageSize)
            );

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
    }, [page,priority]);
      
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
        return <div>Loading tasks...</div>;
    }
    if (error) {
        return <p>{error}</p>;
    }

    return (
        <div>
        <button onClick={logout} >Logout</button>
        <CreateTaskForm onTaskCreated={fetchTasks} />
            <h2>Tasks</h2>

            <select
                onChange={(e) => {
                    setPriority(
                        e.target.value
                            ? Number(e.target.value)
                            : null
                    );
                    setPage(1);
                }}
            >
                <option value="">
                    All
                </option>

                <option value="1">Low</option>
                <option value="2">Medium</option>
                <option value="3">High</option>

            </select>

            {tasks.map(
                (task) => (
                    <div key={task.id}>
                        <h3>{task.title}</h3>
                        <p>{task.description}</p>
                        <p>Priority: {task.priority}</p>
                        <p>Completed: {task.isCompleted ? "Yes" : "No"}</p>
                        
                        {!task.isCompleted && (
                            <button disabled={loading} onClick={() => handleCompleteTask(task.id)}>
                            Complete
                            </button>
                        )}
                        <br />
                        
                        <button disabled={loading} onClick={() => handleDeleteTask(task.id)}>
                            Delete
                        </button>

                    </div>
                )
            )}

            <button onClick={() => setPage(p => p - 1)} disabled={page === 1 || loading}>
                Prev
            </button>
            <span> Page {page} out of {Math.max(page, totalPages)}</span>
            <button onClick={() => setPage(p => p + 1)} disabled={page >= totalPages || loading} >
                Next
            </button>

        </div>
    );
    
};

export default TasksPage;