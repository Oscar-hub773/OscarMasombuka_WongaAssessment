import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { registerUser } from "../services/authService";

function RegisterPage() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
  });

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e) => {
  e.preventDefault();

  try {
    await registerUser(formData);
    alert("Registration successful. Please login.");
    navigate("/");
  } catch (error) {
    console.error(error);
    alert("Registration failed. User may already exist.");
  }
};

  return (
    <div className="container">
      <div className="card">
              <h5 className="text-muted text-center mb-4">Register</h5>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="firstName"
          placeholder="First Name"
          value={formData.firstName}
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="lastName"
          placeholder="Last Name"
          value={formData.lastName}
          onChange={handleChange}
          required
        />
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={formData.email}
          onChange={handleChange}
          pattern="^[^\s@]+@[^\s@]+\.[^\s@]{2,}$"
          title="Please enter a valid email address (e.g. user@example.com)"
          required
        />
        <input
          type="password"
          name="password"
          placeholder="Password"
          value={formData.password}
          onChange={handleChange}
          pattern="^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
          title="Password must be at least 8 characters, include one uppercase letter, one number, and one special character."
          required
        />
        <button type="submit">Register</button>
      </form>
      <div className="link">
        <a href="/">Already have an account? Login</a>
      </div>
      </div>
    </div>
  );
}

export default RegisterPage;