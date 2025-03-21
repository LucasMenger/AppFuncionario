// src/pages/Employees/EmployeesEdit.jsx
import React, { useEffect, useState } from 'react';
import { getEmployeeById, updateEmployee } from '../../api/employeesApi';
import { useParams, useNavigate } from 'react-router-dom';

const EmployeesEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [employee, setEmployee] = useState(null);

  useEffect(() => {
    const fetchEmployee = async () => {
      try {
        const data = await getEmployeeById(id);
        setEmployee(data);
      } catch (error) {
        console.error('Erro ao buscar funcionário:', error);
      }
    };

    fetchEmployee();
  }, [id]);

  const handleChange = (e) => {
    setEmployee({ ...employee, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await updateEmployee(id, employee);
      alert('Funcionário atualizado com sucesso!');
      navigate('/employees');
    } catch (error) {
      console.error('Erro ao atualizar funcionário:', error);
    }
  };

  if (!employee) return <p className="text-center text-gray-600">Carregando...</p>;

  return (
    <div className="container mx-auto p-6 max-w-lg bg-white shadow-md rounded-lg">
      <h1 className="text-2xl font-semibold text-gray-800 mb-4 text-center">Editar Funcionário</h1>

      <form onSubmit={handleSubmit} className="space-y-4">
        <input
          type="text"
          name="firstName"
          value={employee.firstName}
          onChange={handleChange}
          required
          className="w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500"
        />
        <input
          type="text"
          name="lastName"
          value={employee.lastName}
          onChange={handleChange}
          required
          className="w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500"
        />
        <input
          type="email"
          name="email"
          value={employee.email}
          onChange={handleChange}
          required
          className="w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500"
        />
        <input
          type="text"
          name="documentNumber"
          value={employee.documentNumber}
          onChange={handleChange}
          required
          className="w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500"
        />

        <div className="flex justify-between">
          <button
            type="button"
            onClick={() => navigate('/employees')}
            className="bg-gray-500 hover:bg-gray-700 text-white font-bold py-2 px-4 rounded"
          >
            Voltar
          </button>
          <button
            type="submit"
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
          >
            Atualizar
          </button>
        </div>
      </form>
    </div>
  );
};

export default EmployeesEdit;
