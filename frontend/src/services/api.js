import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5095/api", // Replace with your backend URL
});

export default api;
