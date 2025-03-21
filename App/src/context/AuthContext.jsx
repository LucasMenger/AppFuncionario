// src/context/AuthContext.jsx
import React, { createContext, useState, useContext } from 'react';

const AuthContext = createContext();

export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({ children }) => {
  const [jwt, setJwt] = useState(localStorage.getItem('jwt') || null);

  const saveToken = (token) => {
    setJwt(token);
    localStorage.setItem('jwt', token);
  };

  const logout = () => {
    setJwt(null);
    localStorage.removeItem('jwt');
  };

  return (
    <AuthContext.Provider value={{ jwt, saveToken, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
