import { jwtDecode } from "jwt-decode";
import { getToken, removeToken } from "../../../utils/token";
import type { JwtPayload } from "../types/jwt.types";

export const isAuthenticated = (): boolean => {
    const token = getToken();

    if (!token) {
        return false;
    }

    try {
        const decoded = jwtDecode<JwtPayload>(token);
        const currentTime = Date.now() / 1000;
        const isExpired = decoded.exp < currentTime;

        if (isExpired) {
            removeToken();
            return false;
        }
        return true;
    } catch {
        removeToken();
        return false;
    }
};

export const getCurrentUser = (): JwtPayload | null => {
    const token = getToken();
    if (!token) {
        return null;
    }
    try {
        return jwtDecode<JwtPayload>(token);
    } catch {
        return null;
    }
};