import axios from "axios";

export const calculateGrowth = (params) =>
  axios.get("/api/growth", {
    params,
  });

export const saveContributions = (params) =>
  axios.post("/api/contributions", params);
