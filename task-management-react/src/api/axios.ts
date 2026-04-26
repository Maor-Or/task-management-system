import axios from "axios";

const axiosInstance = axios.create({
    baseURL: "https://localhost:5184/api",
    headers: {
        "Content-Type": "application/json",
    },
});

export default axiosInstance;