import React, { useEffect, useState } from 'react';

export default function AdminCheckingInsList({ checkingIns }) {
    const [checkingInsState, setCheckingInsState] = useState([]);

    useEffect(() => {
        setCheckingInsState(checkingIns);
    }, [checkingIns]);

    const editCheckingInHandler = (checkingInId) => {
        console.log(`Editing checking in with ID: ${checkingInId}`);
    };

    const deleteCheckingInHandler = (checkingInId) => {
        console.log(`Deleting checking in with ID: ${checkingInId}`);

        fetch(`/api/check-ins/delete/${checkingInId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text();
            })
            .then(message => {
                console.log('Delete successful:', message);
                
                setCheckingInsState(prevCheckingIns => prevCheckingIns.filter(checkingIn => checkingIn.id !== checkingInId));
            })
            .catch(error => {
                console.error('Error deleting checking in:', error);
            });
    };

    const formatDate = (dateString) => {
        const options = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', hour12: false };
        return new Date(dateString).toLocaleString('hu-HU', options).replace(',', '');
    };

    return (
        <>
            <h1>Checking Ins</h1>
            {checkingInsState.length > 0 ? (
                <table className={"tableList"}>
                    <thead>
                    <tr>
                        <th>ID</th>
                        <th>User ID</th>
                        <th>Start Date</th>
                        <th>End Date</th>
                        <th>Edit</th>
                        <th>Delete</th>
                    </tr>
                    </thead>
                    <tbody>
                    {checkingInsState.map(checkingIn => (
                        <tr key={checkingIn.id}>
                            <td>{checkingIn.id}</td>
                            <td>{checkingIn.userId}</td>
                            <td>{formatDate(checkingIn.startDate)}</td>
                            <td>{formatDate(checkingIn.endDate)}</td>
                            <td>
                                <button onClick={() => editCheckingInHandler(checkingIn.id)}>Edit</button>
                            </td>
                            <td>
                                <button onClick={() => deleteCheckingInHandler(checkingIn.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            ) : (
                <p>No checking ins available to display.</p>
            )}
        </>
    );
}
