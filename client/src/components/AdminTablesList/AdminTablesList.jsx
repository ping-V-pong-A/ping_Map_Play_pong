import React, { useEffect, useState } from 'react';
import AdminTableEditor from "../../components/AdminTableEditor/AdminTableEditor.jsx";

const getTables = () => fetch('/api/tables')
    .then(resp => {
        if (!resp.ok) {
            throw new Error('Network response was not ok');
        }
        return resp.json();
    })
    .catch(error => {
        console.error('Error fetching tables:', error);
    });

const AdminTablesList = (props) => {
    const [allTables, setAllTables] = useState([]);
    const [editing, setEditing] = useState(false);
    const [editTable, setEditTable] = useState(null);

    useEffect(() => {
        getTables().then(data => setAllTables(data))
    }, [editing]);

    const goBackHandler = () =>  props.onSaveData();

    const saveDataHandler = () => setEditing(false);   
 
    const tableEditorHandler = (tableId) => {
        const tableToEdit = allTables.find(table => table.id === tableId);
        setEditTable(tableToEdit);
        setEditing(true);
    }

    return (
        <>
            {editing ? (
                <div>
                    <AdminTableEditor table={editTable} onSaveData={saveDataHandler}/>
                </div>
            ) : (
                <>
                    <h1>Tables</h1>
                    {allTables && allTables.length > 0 ? (
                        <table>
                            <thead>
                            <tr>
                                <th>Name</th>
                                <th>Latitude</th>
                                <th>Longitude</th>
                                <th>Checking Ins</th>
                                <th>Matches</th>
                                <th>Pair Matches</th>
                                <th>Edit</th>
                                <th>Delete</th>
                            </tr>
                            </thead>
                            <tbody>
                            {allTables.map(table => (
                                <tr key={table.id}>
                                    <td>{table.name}</td>
                                    <td>{table.lat}</td>
                                    <td>{table.lon}</td>
                                    <td>
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
                                    </td>
                                    <td>
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
                                    </td>
                                    <td>
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
                                    </td>
                                    <td>
                                        <button onClick={() => tableEditorHandler(table.id)}>Edit Table</button>
                                    </td>
                                    <td>
                                        <button onClick={() => deleteTableHandler(table.id)}>Delete Table</button>
                                    </td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    ) : (
                        <p>No tables available to display.</p>
                    )}
                    <button onClick={goBackHandler}>Back</button>
                </>
            )}
        </>
    );

}

export default AdminTablesList;
