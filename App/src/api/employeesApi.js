// src/api/employeesApi.js
import axios from 'axios';

const API_URL = 'http://localhost:5114/api/Employees';

// Função auxiliar para obter o token do localStorage
const getAuthHeader = () => {
    const token = localStorage.getItem('jwt');
    console.log("Token JWT:", token); // Verificar se o token está correto
    return token ? { Authorization: `Bearer ${token}` } : {};
  };
  

export const getEmployees = async () => {
  const response = await axios.get(API_URL, { headers: getAuthHeader() });
  return response.data.data;
};
export const createEmployee = async (employee) => {
  try {
    // Verifique o URL da API
    const response = await axios.post('http://localhost:5114/api/Employees', employee, {
      headers: getAuthHeader() 
    });
    return response.data;
  } catch (error) {
    console.error("Erro ao criar funcionário:", error);
    throw error;  // Lançar o erro para que o componente possa lidar com ele
  }
};


export const getEmployeeById = async (id) => {
  const response = await axios.get(`${API_URL}/${id}`, { headers: getAuthHeader() });
  return response.data;
};


export const updateEmployee = async (id, employee) => {
  const response = await axios.put(`${API_URL}/${id}`, employee, { headers: getAuthHeader() });
  return response.data;
};

export const deleteEmployee = async (id) => {
  await axios.delete(`${API_URL}/${id}`, { headers: getAuthHeader() });
};
