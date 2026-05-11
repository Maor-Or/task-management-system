import { useState } from "react";
import { loginUser } from "../api/auth.api";
import { setToken } from "../../../utils/token";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const LoginPage = () => {

  const [email, setEmail] =
    useState("test@mail");

  const [password, setPassword] =
    useState("Test1234");

  const [error, setError] =
    useState("");
  
  const navigate = useNavigate();

  const handleSubmit =
    async (e: React.SyntheticEvent) => {

      e.preventDefault();

      setError("")

      try {

        const token =
          await loginUser({
            email,
            password
          });

        setToken(token);

        toast.info("Login successful");

        navigate("/tasks", {replace: true});

      } catch (err: any) {
            console.log("LOGIN ERROR:", err);
            setError(err?.response?.data?.message || "Login failed");
      }
    };

  const [isVisible, setIsVisible] = useState(true);

  const handleToggle = async () => {
    setIsVisible(prev => !prev);
  }
  
  return (
    <div>

      <h2>Login</h2>

      <div style = {{display: "flex", alignItems: "center", gap: "10px"}}>
      {isVisible && (
        <p> Test login user credentials: Email: test@mail. Password: Test1234</p>
      )}
      <button onClick={handleToggle}>
        {isVisible ? "hide" : "show"}
      </button>
      </div>
      
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
      <h3>Don't have an account yet?</h3>
      <Link to="/register">
        Register
      </Link>

    </div>
  );
};

export default LoginPage;