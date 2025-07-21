import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7220/api",
});

export default api;
