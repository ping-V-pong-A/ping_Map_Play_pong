import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useProfile } from '../../contexts/ProfileContext';
import SignInForm from '../../components/SignInForm/SignInForm.jsx';

const postSignIn = (user) => {
    return fetch('/api/auth/sign-in', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include',
        body: JSON.stringify(user)
    })
        .then(res => {
            if (!res.ok) {
                throw new Error("Invalid username or password.");
            }
            return res.json();
        })
        .catch(err => {
            console.error('Error:', err);
            throw err;
        });
};

export default function SignIn() {
    const navigate = useNavigate();
    const { setProfile, login } = useProfile();
    const [errorMessage, setErrorMessage] = useState("");

    const handleSignIn = (user) => {
        setErrorMessage("");
        postSignIn(user)
            .then(data => {
                login();
                setProfile(data);
                console.log(data);
                navigate('/tables');
                return data;
            })
            .catch(err => {
                setErrorMessage(err.message);
            });
    };

    const props = {
        onSave: handleSignIn,
        onCancel: () => navigate('/')
    };

    return (
        <>
            <SignInForm {...props} />
            {errorMessage && <div className="error-message">{errorMessage}</div>}
        </>
    );
}
