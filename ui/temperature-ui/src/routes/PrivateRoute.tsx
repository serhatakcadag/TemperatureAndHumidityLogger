import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const PrivateRoute: React.FC = () => {
    const authToken = localStorage.getItem('authToken');
    return authToken ? <Outlet /> : <Navigate to="/login" />;
};

export default PrivateRoute;