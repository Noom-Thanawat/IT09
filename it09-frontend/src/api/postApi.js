import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7207" 
});

export const seedPost = () => api.post("/api/posts/seed");

export const getPost = (id) => api.get(`/api/posts/${id}`);

export const createComment = (id, payload) =>
  api.post(`/api/posts/${id}/comments`, payload);