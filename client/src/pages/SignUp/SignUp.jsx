import React from 'react';
import {useNavigate} from "react-router-dom";

import SignUpForm from "../../components/SignUpForm/SignUpForm.jsx"

const postSignUp = (user) => fetch('/api/Auth/Registration', {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(user)
    }).then(res => {
        if (!res.ok) {
            throw new Error(`HTTP error! status: ${res.status}`);
        }
        return res.ok
    }).catch(err => {
        console.error('Error:', err);
});

export default function SignUp() {
    const navigate = useNavigate();

    const handleSignIn = (user) => {
        postSignUp(user).then( res => res ? navigate("/solar-watch") : navigate("/sign-up"))
    }

    const props = {
        onSave: handleSignIn,
        onCancel: _ => navigate("/")
    }
    return (
        <>
            <SignUpForm {...props}/>
        </>
    );
}