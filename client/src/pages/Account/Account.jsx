import React, { useEffect, useState } from "react";
import { useProfile } from "../../contexts/ProfileContext.jsx";
import Loading from "../../components/Loading/Loading.jsx";
import { useNavigate } from "react-router-dom";
import AddMatch from "../../components/AddMatch/AddMatch.jsx";

const getUserMatches = (id) =>
    fetch(`api/matches/user/${id}`)
        .then(resp => resp.json())
        .catch(error => console.error('Error:', error));

export default function Account() {
    const navigate = useNavigate();
    const { profile } = useProfile();
    const [matches, setMatches] = useState(null);
    const [addingNewMatch, setAddingNewMatch] = useState(false);

    const refreshMatches = () => {
        if (profile) {
            getUserMatches(profile.id)
                .then(data => setMatches(data))
                .catch(error => console.error('Error fetching matches:', error));
        }
    };

    useEffect(() => {
        if (profile) {
            refreshMatches();
        } else {
            navigate('/sign-in');
        }
    }, [profile, navigate]);

    const getRankString = (rank) => {
        switch(rank) {
            case 0: return 'Rookie';
            case 1: return 'Expert';
            case 2: return 'Beginner';
            case 3: return 'Intermediate';
            case 4: return 'Senior';
            default: return 'None';
        }
    };

    const addMatchHandler = () => {
        setAddingNewMatch(true);
    }

    return profile ? (
        <div className='container'>
            <h1>Account</h1>
            {addingNewMatch ? (
                <AddMatch
                    onGoBack={() => {
                        setAddingNewMatch(false);
                        refreshMatches();
                    }}
                />
            ) : (
                <>
                    <button onClick={addMatchHandler}>Add new match</button>
                    <button>Check in somewhere</button>
                    <button>Favourite tables</button>
                    <table>
                        <thead>
                        <tr>
                            <th>Username</th>
                            <td>{profile.userName}</td>
                        </tr>
                        <tr>
                            <th>Rank</th>
                            <td>{getRankString(profile.rank)}</td>
                        </tr>
                        <tr>
                            <th>Matches</th>
                            <td>
                                {matches ? (
                                    matches.length > 0 ? (
                                        matches.map(match => (
                                            <div key={match.id}>
                                                Match {match.id}
                                            </div>
                                        ))
                                    ) : (
                                        <div>No matches found.</div>
                                    )
                                ) : (
                                    <Loading/>
                                )}
                            </td>
                        </tr>
                        </thead>
                    </table>
                </>
            )}
        </div>
    ) : null;
}
