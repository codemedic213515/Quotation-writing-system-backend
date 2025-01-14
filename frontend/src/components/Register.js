import React, { useState } from "react";
import api from "../services/api";

const Register = () => {
  const [formData, setFormData] = useState({
    code: "",
    name: "",
    email: "",
    pwd: "",
    role: "user",
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await api.post("/auth/register", formData);
      alert(
        `Registered successfully. Your init code: ${response.data.user.Init}`,
      );
    } catch (err) {
      alert(err.response.data || "An error occurred.");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input name="code" placeholder="Code" onChange={handleChange} />
      <input name="name" placeholder="Name" onChange={handleChange} />
      <input name="email" placeholder="Email" onChange={handleChange} />
      <input
        name="pwd"
        placeholder="Password"
        type="password"
        onChange={handleChange}
      />
      <select name="role" onChange={handleChange}>
        <option value="user">User</option>
        <option value="admin">Admin</option>
      </select>
      <button type="submit">Register</button>
    </form>
  );
};

export default Register;
