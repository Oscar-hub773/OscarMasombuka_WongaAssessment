const API_BASE = "https://localhost:7147/api/Authentication";
const USER_API_ENDPOINT = "https://localhost:7147/api/Users";


export async function registerUser(formData) {
  const response = await fetch(`${API_BASE}/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  });

  if (!response.ok) {
    throw new Error("Registration failed");
  }

  return response.json();
}

export async function loginUser(credentials) {
  const response = await fetch(`${API_BASE}/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(credentials),
  });

  if (!response.ok) {
    throw new Error("Login failed");
  }

  return response.json();
}

export async function getCurrentUser(token) {
  const response = await fetch(
    `${USER_API_ENDPOINT}/GetCurrentLoggedInUserDetails`,
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

  return response.json();
}