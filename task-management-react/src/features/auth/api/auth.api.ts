import axios from "../../../api/axios";
import type {
  LoginRequest,
  RegisterRequest,
  ApiResponse
} from "../types/auth.types";

export const loginUser = async (
  data: LoginRequest
): Promise<string> => {

  const response =
      await axios.post<ApiResponse<{token: string}>>(
      "/auth/login",
      data
    );

  return response.data.data.token;
};

export const registerUser = async (
  data: RegisterRequest
): Promise<void> => {

  await axios.post<ApiResponse<void>>(
    "/auth/register",
    data
  );
};