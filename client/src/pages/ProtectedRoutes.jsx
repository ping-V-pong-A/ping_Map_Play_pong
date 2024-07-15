import React, {useEffect} from 'react';
import {Navigate, Outlet, useNavigate} from 'react-router-dom';
import { useProfile } from '../contexts/ProfileContext.jsx'

export default function ProtectedRoutes() {
    const { login, logout } = useProfile();

    const navigate = useNavigate();
    const isLoggedIn = localStorage.getItem('isLoggedIn');
    const logoutTime = localStorage.getItem('logoutTime');
    
    useEffect(() => {

        if (!isLoggedIn || (logoutTime && new Date().getTime() > logoutTime)) {
            localStorage.removeItem('isLoggedIn');
            localStorage.removeItem('logoutTime');
            logout();
        }
    }, [navigate]);

    return isLoggedIn ? <Outlet /> : <Navigate to="/sign-in" />;
};