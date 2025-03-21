import React, { useState } from 'react';
import { createEmployee } from '../../api/employeesApi';
import { useNavigate } from 'react-router-dom';

const EmployeeCreate = () => {
  const [employee, setEmployee] = useState({
    nome: '',
    sobrenome: '',
    email: '',
    cpf: '',
    telefone: '',
    senha: '',
    gerenteId: 0, // Valor padrão 0
    permissao: 'Funcionario', // Valor padrão 'Funcionario'
  });

  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEmployee({ ...employee, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // O valor de permissao já está correto, não há necessidade de mapear
    const employeeData = {
      ...employee,
    };

    try {
      const response = await createEmployee(employeeData);
      console.log('Funcionário criado com sucesso:', response);
      navigate('/employees'); // Redireciona para a lista de funcionários
    } catch (error) {
      console.error('Erro ao criar funcionário:', error);
    }
  };

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-2xl font-semibold text-gray-800 mb-4">Criar Funcionário</h1>

      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label htmlFor="nome" className="block text-sm font-medium text-gray-700">Nome</label>
          <input
            type="text"
            id="nome"
            name="nome"
            value={employee.nome}
            onChange={handleChange}
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md"
            required
          />
        </div>

        <div>
          <label htmlFor="sobrenome" className="block text-sm font-medium text-gray-700">Sobrenome</label>
          <input
            type="text"
            id="sobrenome"
            name="sobrenome"
            value={employee.sobrenome}
            onChange={handleChange}
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md"
            required
          />
        </div>

        <div>
          <label htmlFor="email" className="block text-sm font-medium text-gray-700">Email</label>
          <input
            type="email"
            id="email"
            name="email"
            value={employee.email}
            onChange={handleChange}
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md"
            required
          />
        </div>

        <div>
          <label htmlFor="cpf" className="block text-sm font-medium text-gray-700">CPF</label>
          <input
            type="text"
            id="cpf"
            name="cpf"
            value={employee.cpf}
            onChange={handleChange}
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md"
            required
          />
        </div>

        <div>
          <label htmlFor="telefone" className="block text-sm font-medium text-gray-700">Telefone</label>
          <input
            type="text"
            id="telefone"
            name="telefone"
            value={employee.telefone}
            onChange={handleChange}
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md"
          />
        </div>

        <div>
          <label htmlFor="senha" className="block text-sm font-medium text-gray-700">Senha</label>
          <input
            type="password"
            id="senha"
            name="senha"
            value={employee.senha}
            onChange={handleChange}
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md"
            required
          />
        </div>

        <div>
          <label htmlFor="gerenteId" className="block text-sm font-medium text-gray-700">Gerente ID</label>
          <input
            type="number"
            id="gerenteId"
            name="gerenteId"
            value={employee.gerenteId}
            onChange={handleChange}
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md"
          />
        </div>

        <div>
          <label htmlFor="permissao" className="block text-sm font-medium text-gray-700">Permissão</label>
          <select
            id="permissao"
            name="permissao"
            value={employee.permissao}
            onChange={handleChange}
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md"
            required
          >
            <option value="Funcionario">Funcionário</option>
            <option value="Lider">Líder</option>
            <option value="Diretor">Diretor</option>
          </select>
        </div>

        <div className="mt-4">
          <button
            type="submit"
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
          >
            Criar Funcionário
          </button>
        </div>
      </form>
    </div>
  );
};

export default EmployeeCreate;
