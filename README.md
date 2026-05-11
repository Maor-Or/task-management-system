Task Management System (Full Stack)

A full-stack task management application built with ASP.NET Core Web API and React, featuring JWT authentication, role-based security, and full CRUD operations with pagination and filtering.


Features:
Authentication & Security
JWT-based authentication
Secure login and registration
Protected routes (frontend + backend)
User-specific data isolation
Task Management
Create, update, delete tasks
Mark tasks as complete
Priority-based task system
User-specific task ownership
Pagination support
Filtering by priority
JWT token decoding & validation
Auto logout on expired tokens
Frontend (React)
Feature-based architecture
Protected routes system
Axios API layer with interceptors
Toast notifications (UX feedback)
Responsive task UI
Backend (.NET)
Clean architecture (Domain / Application / Infrastructure)
Repository pattern
Service layer separation
Global exception handling middleware
Swagger API documentation


Backend:
ASP.NET Core Web API
Entity Framework Core
JWT Authentication
SQL Server
Clean Architecture


Frontend:
React + TypeScript
Axios
React Router
React Toastify
Feature-based architecture
Authentication Flow
User registers / logs in
Backend issues JWT token
Token stored in localStorage
Axios attaches token to requests
Backend validates token per request
User-specific data returned


Architecture Overview:

Frontend (React)
↓
Feature Modules (Auth / Tasks)
↓
API Layer (Axios)
↓
Backend API (.NET)
↓
Controller Layer
↓
Service Layer
↓
Repository Layer
↓
Database


Future Improvements:
React Query integration
Dark mode
Real-time updates (SignalR)
Docker deployment
CI/CD pipeline