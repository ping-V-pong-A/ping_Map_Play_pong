import React, {useEffect, useState} from "react";
import {useProfile} from "../../contexts/ProfileContext.jsx";
import Loading from "../../components/Loading/Loading.jsx";
import {useNavigate} from "react-router-dom";

const getUserData = (id) => fetch(`api/User/users/id/${id*1}/`)
    .then(resp => resp.json())
    .catch(error => console.error('Error:', error))

export default function Account() {
    const navigate = useNavigate();
    const {profile} = useProfile();

    useEffect(() => {
        getUserData(profile.id)
    }, []);
    
    return profile ? (
        <>
            <button onClick={_ => console.log(profile)}>na</button>
            <div className='container'>
                <h1>Account</h1>
                <table>
                    <tr>
                        <th>Username</th>
                        <td>{profile.identityUser.userNamme}</td>
                    </tr>
                    <tr>
                        <th>Email</th>
                        <td>{profile.email}</td>
                    </tr>
                </table>
            </div>
        </>
        ) : navigate('/sign-in')
    
}