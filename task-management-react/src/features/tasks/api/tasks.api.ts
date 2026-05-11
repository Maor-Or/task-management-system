import axios from "../../../api/axios";
import type { Task, PaginatedResponse, ApiResponse } from "../types/task.types";

export const getTasks = async () => {
    const response =
        await axios.get<
            ApiResponse<
                PaginatedResponse<Task>
            >
            >("/tasks");
    return response.data.data;
}