import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5162/api',
});

export const login = (name, password) =>
  api.post('/user/login', { name, password });

export const getVendorEvents = (vendorId) =>
  api.get(`/event/vendor/${vendorId}`);

export const getClientEvents = (userId) =>
  api.get(`/booking/user/${userId}/events`);

export default api;