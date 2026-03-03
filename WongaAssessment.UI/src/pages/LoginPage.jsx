import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";
import { loginUser } from "../services/authService";

function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

 const handleSubmit = async (e) => {
  e.preventDefault();

  try {
    const data = await loginUser({ email, password });

    login(data.token);
    navigate("/user");
  } catch (error) {
    console.error(error);
    alert("Invalid email or password");
  }
};

  return (
    
    <div className="container">
      <div className="card">
        <h5 className="text-muted text-center mb-4">Login</h5>
      <form onSubmit={handleSubmit}>
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />

        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />

        <button type="submit">Login</button>
      </form>
        <div className="link">
        <a href="/register">Don't have an account? Register</a>
      </div>
      </div>
    </div>
  );
}
export default LoginPage;