import {BrowserRouter, Routes,Route } from "react-router-dom";
import LoginPage from "../features/auth/pages/LoginPage";
import RegisterPage from "../features/auth/pages/RegisterPage";
import ProtectedRoute from "./ProtectedRoute";
import TasksPage from "../features/tasks/pages/TasksPage";
// import { Navigate } from "react-router-dom";

const AppRouter = () => {

  return (

    <BrowserRouter>

      <Routes>

        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/tasks" element={<ProtectedRoute><TasksPage /></ProtectedRoute>} />
        {/* <Route path="/yo" element={<Navigate to="/login" replace/>} /> */}
        {/* <Route path="/yoyo" element={<div><p>hello suckas</p></div>} /> */}
        
        
      </Routes>

    </BrowserRouter>

  );
};

export default AppRouter;