import React, { useState, useEffect } from 'react';

const UsersList = (props) => {

    const [allUsers, setAllUsers] = useState([]);
    const [refreshNeeded, setRefreshNeeded] = useState(false)

    useEffect(() => {
        fetch('/api/users')
            .then(resp => {
                if (!resp.ok) {
                    throw new Error('Network response was not ok');
                }
                return resp.json();
            })
            .then(data => {
                setAllUsers(data);
            })
            .catch(error => {
                console.error('Error fetching users:', error);
            });
    }, [refreshNeeded]);


    const goBackHandler = () =>{
        props.onSaveData();
    }

    const deleteUserHandler = (event) => {
        fetch(`/api/User/users/id/${event.target.id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        })
            .then(resp => {
                if (!resp.ok) {
                    throw new Error('Network response was not ok');
                }
                console.log('User deleted successfully');
                setRefreshNeeded(true);
            })
            .catch(error => {
                console.error('Error deleting user:', error);
            });
    };



    return (
        <>
            <h1>Users</h1>
            <table>
                <thead>
                <tr>
                    <th>ID</th>
                    <th>Registration Date</th>
                    <th>Rank</th>
                    <th>Checked-in Tables</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                {allUsers && allUsers.map(user => (
                    <tr key={user.id}>
                        <td>{user.id}</td>
                        <td>{new Date(user.registrationDate).toLocaleDateString()}</td>
                        <td>{user.rank}</td>
                        <td>
                            <ul>
                                {user.checkedInTables && user.checkedInTables.map(table => (
                                    <li key={table.id}>{table.name}</li>
                                ))}
                            </ul>
                        </td>
                        <td>
                            <button onClick={deleteUserHandler} id={user.id}>Delete user</button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
            <button onClick={goBackHandler}>Back</button>
        </>
    );

}

export default UsersList;