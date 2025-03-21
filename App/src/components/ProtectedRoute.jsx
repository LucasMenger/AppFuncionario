import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext'; 

const ProtectedRoute = ({ element }) => {
  const { jwt } = useAuth(); 

  if (!jwt) {
    return <Navigate to="/" replace />;
  }

  return element; 
};

export default ProtectedRoute;
