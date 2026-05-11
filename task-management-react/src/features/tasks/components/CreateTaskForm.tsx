import { useState } from "react";
import { createTask } from "../api/tasks.api";

type Props = {
    onTaskCreated: () => void;
};

const CreateTaskForm = ({ onTaskCreated }: Props) => {
    const [title, setTitle] = useState("");

    const [description, setDescription] = useState("");

    const today = new Date().toISOString().split("T")[0];
    const [dueDate, setDueDate] = useState(today);

    const [priority, setPriority] = useState(1);
    
    const [error, setError] = useState("");
    

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");

        try {
            await createTask({
                title,
                description,
                dueDate,
                priority
            });
            
            setTitle("");
            setDescription("");
            setDueDate(today);
            setPriority(1);
            
            onTaskCreated();
        } catch {
            setError("Failed to create task");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h3>Create task</h3>
            {error && <p>{error}</p>}

            <input
                type="text"
                placeholder="Title"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required />
            <br />

            <textarea
                placeholder="Description"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
            />
            <br />

            <input
                type="date"
                value={dueDate}
                onChange={(e) =>
                    setDueDate(
                        e.target.value
                    )}
                required
            />

            <select
                value={priority}
                onChange={(e) => setPriority(Number(e.target.value))}
            >
                <option value={1}>
                    Low
                </option>
                <option value={2}>
                    Medium
                </option>
                <option value={3}>
                    High
                </option>
            </select>
            <br />

            <button type="submit">
                Create Task
            </button>
        </form>
    );
};
export default CreateTaskForm;
