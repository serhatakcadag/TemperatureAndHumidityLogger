import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const PublicRoute: React.FC = () => {
    const authToken = localStorage.getItem('authToken');
    return authToken ? <Navigate to="/" /> : <Outlet />;
};

export default PublicRoute;