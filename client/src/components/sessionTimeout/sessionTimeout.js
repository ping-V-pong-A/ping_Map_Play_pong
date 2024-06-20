import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const sessionTimeout = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const isLoggedIn = localStorage.getItem('isLoggedIn');
       
        const logoutTime = localStorage.getItem('logoutTime');

        if (!isLoggedIn || (logoutTime && new Date().getTime() > logoutTime)) {
            localStorage.removeItem('isLoggedIn');
            localStorage.removeItem('logoutTime');
            navigate('/signin');
        }
    }, [navigate]);
};

export default sessionTimeout;

