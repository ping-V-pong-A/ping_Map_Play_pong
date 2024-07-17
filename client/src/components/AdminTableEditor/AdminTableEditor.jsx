import React, { useState } from 'react';

const AdminTableEditor = (props) => {
    
    const table = props.table;
    const [editingTable, setEditingTable] = useState({
        name: table.name,
        lat: table.lat,
        lon: table.lon
    });

    const goBackHandler = () =>{
        props.onSaveData();
        
    }
    
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setEditingTable(prevState => ({
            ...prevState,
            [name]: value
        }));
    };
    
    

    const handleSubmit = (e) => {
        e.preventDefault();
        
        fetch(`/api/Table/tables/id/${table.id}`, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(editingTable),
        })
            .then(resp => {
                if (!resp.ok) {
                    throw new Error('Network response was not ok');
                }
                return resp.json();
            })
            .then(data => {
                console.log('Table updated successfully:', data);
                
                goBackHandler();
            })
            .catch(error => {
                console.error('Error updating table:', error);
            });
    };

    return (
        <>
            {table ? (
                <div>
                    <h2>Edit Table: {table.name}</h2>
                    <form onSubmit={handleSubmit}>
                        <label>Name:</label>
                        <input
                            type="text"
                            name="name"
                            value={editingTable.name}
                            onChange={handleInputChange}
                        />
                        <br />

                        <label>Latitude:</label>
                        <input
                            type="number"
                            name="lat"
                            value={editingTable.lat}
                            onChange={handleInputChange}
                        />
                        <br />

                        <label>Longitude:</label>
                        <input
                            type="number"
                            name="lon"
                            value={editingTable.lon}
                            onChange={handleInputChange}
                        />
                        <br />

                        <h3>Checking Ins:</h3>
                        {table.checkingIns && table.checkingIns.length > 0 ? (
                            <ul>
                                {table.checkingIns.map(checkIn => (
                                    <li key={checkIn.id}>
                                        <p>Checked-in User ID: {checkIn.userId}</p>
                                    </li>
                                ))}
                            </ul>
                        ) : (
                            <p>No check-ins for this table.</p>
                        )}

                        <h3>Matches:</h3>
                        {table.matches && table.matches.length > 0 ? (
                            <ul>
                                {table.matches.map(match => (
                                    <li key={match.id}>
                                        <p>Match ID: {match.id}</p>
                                    </li>
                                ))}
                            </ul>
                        ) : (
                            <p>No matches for this table.</p>
                        )}

                        <h3>Pair Matches:</h3>
                        {table.pairMatches && table.pairMatches.length > 0 ? (
                            <ul>
                                {table.pairMatches.map(pairMatch => (
                                    <li key={pairMatch.id}>
                                        <p>Pair Match ID: {pairMatch.id}</p>
                                    </li>
                                ))}
                            </ul>
                        ) : (
                            <p>No pair matches for this table.</p>
                        )}

                        <button type="submit">Save</button>
                    </form>
                </div>
            ) : (
                <p>No table selected for editing.</p>
            )}
            <button onClick={goBackHandler}>Back</button>
        </>
    );
}

export default AdminTableEditor;
