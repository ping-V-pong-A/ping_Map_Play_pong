import React, { useEffect } from 'react';
import { Navigate, Outlet, useNavigate } from 'react-router-dom';
import { useProfile } from '../contexts/ProfileContext.jsx';

export default function ProtectedRoutes() {
    const { login, logout, profile } = useProfile();
    const navigate = useNavigate();
    const isLoggedIn = localStorage.getItem('isLoggedIn');
    const logoutTime = localStorage.getItem('logoutTime');

    useEffect(() => {
        const checkLoginStatus = () => {
            const currentTime = new Date().getTime();
            if (!profile || !isLoggedIn || (logoutTime && currentTime > parseInt(logoutTime))) {
                logout();
                navigate('/sign-in');
            }
        };

        checkLoginStatus();
    }, [isLoggedIn, logoutTime, logout, navigate]);

    return isLoggedIn ? <Outlet /> : <Navigate to="/sign-in" />;
}