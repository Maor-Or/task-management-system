import { removeToken } from "../../../utils/token";

export const logout = () => {
    removeToken();
    window.location.href = "/login";
}