import React, {useEffect, useState} from "react";
import {useProfile} from "../../contexts/ProfileContext.jsx";
import Loading from "../../components/Loading/Loading.jsx";
import {useNavigate} from "react-router-dom";

const getUserData = id => fetch(`api/User/users/id/${id}/`)
    .then(resp => resp.json())
    .catch(error => console.error('Error:', error))

const getUserMatches = id => fetch(`api/Match/matches/user/${id}`)
    .then(resp => resp.json())
    .catch(error => console.error('Error:', error))

export default function Account() {
    const navigate = useNavigate();
    const {profile} = useProfile();
    const [matches, setMatches] = useState(null);

    useEffect(() => {
        profile ?
        getUserMatches(profile.id)
            .then(data => setMatches(data))
            :
            navigate('/sign-in')
    }, []);
    
    return profile ? (
        <>
            <button onClick={_ => console.log(profile)}>na</button>
            <div className='container'>
                <table>
                    <thead>
                        <tr>                            
                            <th>
                                <h1>Account</h1>                            
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th>Username</th>
                            <td>{profile.identityUser.userName}</td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td>{profile.identityUserEmail}</td>
                        </tr>
                        <tr>
                            <th>Id</th>
                            <td>{profile.id}</td>
                        </tr>
                        {matches ? matches.map(match => (
                            <tr key={match.id}>
                                <th>Match {match.id}</th>
                                <td></td>
                            </tr>
                        )) : (
                            <Loading/>
                        )}
                        
                    </tbody>
                </table>
            </div>
        </>
    ) : navigate('/sign-in')

}