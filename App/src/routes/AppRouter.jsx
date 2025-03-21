// src/routes/AppRouter.jsx
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from '../pages/Login';
import Employees from '../pages/Employees/Employees';
import EmployeeEdit from '../pages/Employees/EmployeeEdit';
import EmployeesCreate from '../pages/Employees/EmployeesCreate';
import ProtectedRoute from '../components/ProtectedRoute'; 

const AppRouter = () => (
  <Router>
    <Routes>
      
      <Route path="/" element={<Login />} />

      <Route
        path="/employees"
        element={<ProtectedRoute element={<Employees />} />}
      />
      <Route
        path="/employees/create"
        element={<ProtectedRoute element={<EmployeesCreate />} />}
      />

      <Route
        path="/employee/edit/:id"
        element={<ProtectedRoute element={<EmployeeEdit />} />}
      />
    </Routes>
  </Router>
);

export default AppRouter;
