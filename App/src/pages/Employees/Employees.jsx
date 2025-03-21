import React, { useEffect, useState } from 'react';
import { getEmployees, deleteEmployee } from '../../api/employeesApi';
import { useNavigate } from 'react-router-dom';

const Employees = () => {
  const [employees, setEmployees] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetchEmployees();
  }, []);

  const fetchEmployees = async () => {
    try {
      const data = await getEmployees(); // A função agora retorna apenas os dados
      setEmployees(data); // Atualizando o estado com os dados de funcionários
    } catch (error) {
      console.error('Erro ao buscar funcionários:', error);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Tem certeza que deseja excluir este funcionário?')) {
      try {
        await deleteEmployee(id);
        setEmployees(employees.filter(emp => emp.id !== id)); // Removendo o funcionário da lista
      } catch (error) {
        console.error('Erro ao excluir funcionário:', error);
      }
    }
  };

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-2xl font-semibold text-gray-800 mb-4">Lista de Funcionários</h1>
      
      <button
        onClick={() => navigate('/employees/create')}
        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mb-4"
      >
        + Criar Funcionário
      </button>

      <div className="overflow-x-auto">
        <table className="min-w-full bg-white shadow-md rounded-lg overflow-hidden">
          <thead className="bg-gray-200 text-gray-600 uppercase text-sm leading-normal">
            <tr>
              <th className="py-3 px-6 text-left">ID</th>
              <th className="py-3 px-6 text-left">Nome</th>
              <th className="py-3 px-6 text-left">Email</th>
              <th className="py-3 px-6 text-center">Ações</th>
            </tr>
          </thead>
          <tbody className="text-gray-700 text-sm font-light">
            {employees && employees.length > 0 ? (
              employees.map(emp => (
                <tr key={emp.id} className="border-b border-gray-200 hover:bg-gray-100">
                  <td className="py-3 px-6">{emp.id}</td>
                  <td className="py-3 px-6">{emp.nome} {emp.sobrenome}</td>
                  <td className="py-3 px-6">{emp.email}</td>
                  <td className="py-3 px-6 text-center">
                    <button
                      onClick={() => navigate(`/employees/edit/${emp.id}`)}
                      className="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-3 rounded mr-2"
                    >
                      Editar
                    </button>
                    <button
  onClick={() => {
    if (window.confirm('Você tem certeza que deseja excluir este funcionário?')) {
      handleDelete(emp.id); // Se o usuário confirmar, chama a função de exclusão
    }
  }}
  className="bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-3 rounded"
>
  Excluir
</button>

                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan="4" className="text-center py-3">Nenhum funcionário encontrado</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Employees;
