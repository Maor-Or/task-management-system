# 🧠 Task Management System

A full-stack task management application built with ASP.NET Core Web API and React + TypeScript.

The project demonstrates modern full-stack architecture practices including JWT authentication, protected routes, clean architecture, repository pattern, pagination, filtering, and frontend state management.

---

# 📸 Screenshots

## Registration Page
![Login](https://github.com/user-attachments/assets/13e5d2be-006d-45cf-b13c-04835fdce8d9)

---

## Login Page
![Register](https://github.com/user-attachments/assets/69315055-4440-47dc-8a11-1614140f21b9)

---

## Task Dashboard
![Tasks](https://github.com/user-attachments/assets/390e34e3-e66a-41be-86b6-a9e5f28afca7)

---

# ✨ Features

## 🔐 Authentication & Security

- JWT-based authentication
- Secure login & registration
- Protected frontend and backend routes
- User-specific data isolation
- Automatic logout on expired tokens
- JWT decoding & validation

---

## ✅ Task Management

- Create tasks
- Update tasks
- Delete tasks
- Mark tasks as completed
- Priority-based task system
- Pagination support
- Filtering & sorting
- User-owned task access

---

## 🎨 Frontend Features

- React + TypeScript
- Feature-based architecture
- Axios API layer
- Axios interceptors
- Protected routes
- Toast notifications
- Responsive UI components

---

## ⚙️ Backend Features

- ASP.NET Core Web API
- Clean Architecture
- Repository Pattern
- Service Layer Separation
- Global Exception Handling Middleware
- Swagger API Documentation
- Entity Framework Core

---

# 🏗️ Architecture

```text
Frontend (React)
        ↓
Feature Modules
(Auth / Tasks)
        ↓
API Layer (Axios)
        ↓
Backend API (.NET)
        ↓
Controllers
        ↓
Application Services
        ↓
Repositories
        ↓
SQL Server Database


# 🛠️ Tech Stack

## Backend

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger

## Frontend

- React
- TypeScript
- React Router
- Axios
- React Toastify

---

# 🔄 Authentication Flow

1. User registers or logs in
2. Backend validates credentials
3. JWT token is generated
4. Token stored in `localStorage`
5. Axios automatically attaches token
6. Backend validates JWT on each request
7. Protected resources returned to authenticated user

---

# 🚀 Getting Started

## Backend Setup

```bash
cd TaskManagement.Api
dotnet restore
dotnet run
```

Backend runs on:

```text
https://localhost:5184
```

---

## Frontend Setup

```bash
cd task-management-react
npm install
npm run dev
```

Frontend runs on:

```text
http://localhost:5173
```

---

# 📌 Future Improvements

- React Query integration
- Docker support
- CI/CD pipeline
- Dark mode
- Real-time updates with SignalR
- Unit & integration test expansion

---

# 📚 Learning Goals Demonstrated

This project was built to practice and demonstrate:

- Full-stack application development
- Clean Architecture principles
- JWT authentication flows
- REST API development
- React component architecture
- Frontend/backend integration
- Repository & service patterns
- TypeScript fundamentals
- Pagination & filtering systems

---

# 👨‍💻 Author

Maor Or
