import axios from "axios";

const API = axios.create({
  baseURL: "https://localhost:7149/api"
});

// Attach token
API.interceptors.request.use((req) => {
  const token = localStorage.getItem("token");
  if (token) {
    req.headers.Authorization = `Bearer ${token}`;
  }
  return req;
});

export default API;