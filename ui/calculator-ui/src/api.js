import axios from 'axios';

const api = axios.create({
  baseURL: '/api', // Базовый URL API
  timeout: 5000 // Время ожидания 5 секунд
});

export default api;