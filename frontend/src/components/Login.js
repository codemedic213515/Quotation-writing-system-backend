import React, { useState } from "react";
import api from "../services/api";

const Login = () => {
  const [formData, setFormData] = useState({ email: "", pwd: "" });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await api.post("/auth/login", formData);
      alert(`Welcome, ${response.data.user.name}`);
    } catch (err) {
      alert(err.response.data || "Login failed.");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input name="email" placeholder="Email" onChange={handleChange} />
      <input
        name="pwd"
        placeholder="Password"
        type="password"
        onChange={handleChange}
      />
      <button type="submit">Login</button>
    </form>
  );
};

export default Login;
