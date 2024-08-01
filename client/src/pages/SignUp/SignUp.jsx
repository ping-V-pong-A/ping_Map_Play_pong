import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";
import SignUpForm from "../../components/SignUpForm/SignUpForm.jsx";

const postSignUp = (user, setErrorMessage) => {
    return fetch('/api/auth/sign-up', {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(user)
    })
        .then(res => {
            if (!res.ok) {
                if (res.status === 400) {
                    throw new Error("Username, email, or password is incorrect.");
                } else {
                    throw new Error(`HTTP error! status: ${res.status}`);
                }
            }
            return res.ok;
        })
        .catch(err => {
            console.error('Error:', err);
            setErrorMessage(err.message);
        });
};

export default function SignUp() {
    const navigate = useNavigate();
    const [errorMessage, setErrorMessage] = useState("");

    const handleSignIn = (user) => {
        postSignUp(user, setErrorMessage).then(res => {
            if (res) {
                navigate("/tables");
            } else {
                navigate("/sign-up");
            }
        });
    };

    const props = {
        onSave: handleSignIn,
        onCancel: () => navigate("/"),
    };

    return (
        <>
            <SignUpForm {...props} />
            {errorMessage && <div className="error-message">{errorMessage}</div>}
        </>
    );
}
