import React from 'react';
import ErrorBoundary from './components/ErrorBoundary'; // Importe o ErrorBoundary
import { AuthProvider } from './context/AuthContext';
import AppRouter from './routes/AppRouter';

const App = () => (
  <ErrorBoundary>
    <AuthProvider>
      <AppRouter />
    </AuthProvider>
  </ErrorBoundary>
);

export default App;
