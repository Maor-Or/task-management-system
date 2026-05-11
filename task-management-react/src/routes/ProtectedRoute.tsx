import { Navigate } from "react-router-dom";
import { isAuthenticated } from "../features/auth/utils/auth";
import type { ReactNode } from "react";

type Props = {
    children: ReactNode;
};

const ProtectedRoute = ({ children }: Props) => {
    
    if (!isAuthenticated()) {
        return <Navigate to="/login" replace />
    }

    return children;
};

export default ProtectedRoute;