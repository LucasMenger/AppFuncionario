// src/api/authApi.js
import axios from 'axios';

const API_URL = 'http://localhost:5114/api/'; // URL da sua API de autenticação

export const login = async (email, password) => {
    try {
      const response = await axios.post(`${API_URL}auth/login`, {
        email,
        password
      });
      return response.data; // JWT que a API retorna
    } catch (error) {
      console.error("Erro ao fazer login:", error.response || error.message);
      throw error; // Lança o erro para ser tratado na página
    }
  };