import React, {useEffect} from 'react';
import {useNavigate} from "react-router-dom";
import { useProfile } from '../../contexts/ProfileContext';

import Navbar from "../../components/Navbar/Navbar.jsx";
import SignInForm from "../../components/SignInForm/SignInForm.jsx";

const postSignIn = (user) => fetch('/api/Auth/Login', {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(user)
    }).then(res => {
        if (!res.ok) {
            throw new Error(`HTTP error! status: ${res.status}`);
        }
        return res.json()
    }).catch(err => {
        console.error('Error:', err);    
})

export default function SignIn() {
    const navigate = useNavigate();
    const {profile, setProfile, login } = useProfile();

    const handleSignIn = (user) => {
        postSignIn(user)
            .then(data =>{
            console.log(data)
            setProfile(data)          
            login();
            navigate("/tables")
        }).then(_=> {
            console.log(profile)
        })
    }

    const props = {
        onSave: handleSignIn,
        onCancel: _ => navigate("/")
    }

    return (
        <>
            <SignInForm {...props}/>
        </>
    );
}