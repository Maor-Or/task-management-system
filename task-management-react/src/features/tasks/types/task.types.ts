export interface Task {
    id: string,
    title: string;
    description: string;
    dueDate: string;
    isCompleted: boolean;
    priority: number;
}

export interface PaginatedResponse<T> {
    items: T[];
    totalCount: number;
    page: number;
    pageSize: number;
}

export interface ApiResponse<T> {
    success: boolean;
    message: string;
    data: T;
}