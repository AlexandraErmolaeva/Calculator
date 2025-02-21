import axios from 'axios';

const api = axios.create({
  baseURL: process.env.VUE_APP_API_URL + '/api', // Базовый URL API
  timeout: 5000 // Время ожидания 5 секунд
});

export default api;