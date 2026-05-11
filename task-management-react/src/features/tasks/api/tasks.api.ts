import axios from "../../../api/axios";
import type { Task, PaginatedResponse, ApiResponse, CreateTaskRequest } from "../types/task.types";

export const getTasks = async () => {
    const response =
        await axios.get<
            ApiResponse<
                PaginatedResponse<Task>
            >
        >("/tasks");
    return response.data.data;
};

export const createTask = async (task: CreateTaskRequest) => {
    const response =
        await axios.post("/tasks", task);
    return response.data;
};