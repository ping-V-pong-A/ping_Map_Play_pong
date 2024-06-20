import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useProfile } from '../../contexts/ProfileContext';
import SignInForm from '../../components/SignInForm/SignInForm.jsx';

const postSignIn = (user) => fetch('/api/Auth/Login', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    credentials: 'include',
    body: JSON.stringify(user)
}).then(res => {
    if (!res.ok) {
        throw new Error(`HTTP error! status: ${res.status}`);
    }
    return res.json();
}).catch(err => {
    console.error('Error:', err);
});

export default function SignIn() {
    const navigate = useNavigate();
    const { setProfile, login } = useProfile();



    const handleSignIn = (user) => {
        postSignIn(user)
            .then(data => {
                console.log(data);
                setProfile(data);
                localStorage.setItem('isLoggedIn', true);
                const logoutTime = new Date();
                logoutTime.setMinutes(logoutTime.getMinutes() + 30);
                localStorage.setItem('logoutTime', logoutTime.getTime());
                login();
                navigate('/tables');
            })
            .catch(err => {
                console.error(err);
            });
    };

    const props = {
        onSave: handleSignIn,
        onCancel: () => navigate('/')
    };

    return (
        <>
            <SignInForm {...props} />
        </>
    );
}
