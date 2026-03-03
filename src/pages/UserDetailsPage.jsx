import { useEffect, useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";

function UserPage() {
  const { token, logout } = useContext(AuthContext);
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await fetch(
          "https://localhost:7147/api/Users/GetCurrentUserDetails",
          {
            method: "GET",
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (!response.ok) {
          throw new Error("Failed to fetch user details");
        }

        const data = await response.json();
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
      <h2>User Details</h2>

      <p><strong>First Name:</strong> {user.firstName}</p>
      <p><strong>Last Name:</strong> {user.lastName}</p>
      <p><strong>Email:</strong> {user.email}</p>

      <button onClick={handleLogout}>Logout</button>
    </div>
  </div>
);
}

export default UserPage;