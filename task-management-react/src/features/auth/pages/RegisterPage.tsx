import { useState, type SyntheticEvent } from "react";
import { registerUser } from "../api/auth.api";
import { Link } from "react-router-dom";

const RegisterPage = () => {
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [error, setError] = useState("")
    const [success, setSuccess] = useState("")
    
    const handleSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        setError("")
        setSuccess("");
        // alert("in submithandle");
        
        try {
            await registerUser({ email, password });
            setSuccess("Registration successful. You can now login");
            
        } catch (err: any) {
            console.log("Error registering: ", err);
            setError(err?.response?.data?.message || "REGISTRATION FAILED");
        }
    };

    return (
        <div>
            <h2>Register</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />

                <button type="submit">Register</button>
                {error && (
                    <p style={{ color: "red" }}>
                        {error}
                   </p> 
                )}

                {success && (
                    <p style={{color: "green"}}>
                        {success}
                    </p>
                )}


            </form>  
            <Link to="/login">
                Go to Login
            </Link>
        </div>
    );
};

export default RegisterPage;