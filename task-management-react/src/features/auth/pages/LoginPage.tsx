import { useState } from "react";
import { loginUser } from "../api/auth.api";
import { setToken } from "../../../utils/token";

const LoginPage = () => {

  const [email, setEmail] =
    useState("test@mail");

  const [password, setPassword] =
    useState("Test1234");

  const [error, setError] =
    useState("");

  const handleSubmit =
    async (e: React.SyntheticEvent) => {

      e.preventDefault();

      try {

        const token =
          await loginUser({
            email,
            password
          });

        setToken(token);

        alert("Login successful");

      } catch (err: any) {
            console.log("LOGIN ERROR:", err);
            setError(err?.response?.data?.message || "Login failed");
        }
    };

  return (
    <div>

      <h2>Login</h2>

      <p> Test login user credentials: Email: test@mail. Password: Test1234</p>

      <form onSubmit={handleSubmit}>

        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) =>
            setEmail(e.target.value)
          }
        />

        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) =>
            setPassword(e.target.value)
          }
        />

        <button type="submit">
          Login
        </button>

        {error && (
          <p>{error}</p>
        )}

      </form>

    </div>
  );
};

export default LoginPage;