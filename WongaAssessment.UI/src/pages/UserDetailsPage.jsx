import { useEffect, useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { getCurrentUser } from "../services/authService";

function UserPage() {
  const { token, logout } = useContext(AuthContext);
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
  const fetchUser = async () => {
    try {
      const data = await getCurrentUser(token);
      setUser(data);
    } catch (error) {
      console.error(error);
    }
  };

  fetchUser();
}, [token]);

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  if (!user) {
    return <p>Loading user details...</p>;
  }

return (
  <div className="container">
    <div className="card">
      <h5 className="text-muted">User Details</h5><br></br>
      <p className="text-muted"><strong>First Name:</strong> {user.firstName}</p>
      <p className="text-muted"><strong>Last Name:</strong> {user.lastName}</p>
      <p className="text-muted"><strong>Email:</strong> {user.email}</p>

      <button onClick={handleLogout}>Logout</button>
    </div>
  </div>
);
}

export default UserPage;