import React, { useEffect, useState } from 'react';
import AdminTableEditor from "../../components/AdminTableEditor/AdminTableEditor.jsx";
import AdminCheckingInsList from "../../components/AdminCheckingInsList/AdminCheckingInsList.jsx";

const getTables = () => fetch('/api/tables')
    .then(resp => {
        if (!resp.ok) {
            throw new Error('Network response was not ok');
        }
        return resp.json();
    })
    .catch(error => {
        console.error('Error fetching tables:', error);
        return [];
    });

const AdminTablesList = (props) => {
    const [allTables, setAllTables] = useState([]);
    const [editing, setEditing] = useState(false);
    const [editTable, setEditTable] = useState(null);
    const [showingCheckinginList, setShowingCheckinginList] = useState(false);
    const [checkingIns, setCheckingIns] = useState([]);

    useEffect(() => {
        const fetchTables = async () => {
            const data = await getTables();
            console.log('Fetched tables:', data);
            setAllTables(data);
        };
        fetchTables();
    }, []);

    const goBackHandler = () => props.onSaveData();
    const saveDataHandler = () => setEditing(false);

    const tableEditorHandler = (tableId) => {
        const tableToEdit = allTables.find(table => table.id === tableId);
        setEditTable(tableToEdit);
        setEditing(true);
    };

    const deleteTableHandler = (tableId) => {
        console.log(`Deleting table with ID: ${tableId}`);
    };

    const CheckingInsListHandler = (checkingIns) => {
        setCheckingIns(checkingIns);
        setShowingCheckinginList(true);
    };

    return (
        <>
            {editing ? (
                <div>
                    <AdminTableEditor table={editTable} onSaveData={saveDataHandler} />
                </div>
            ) : (
                <>
                    {showingCheckinginList ? (
                        <div>
                            <AdminCheckingInsList checkingIns={checkingIns} />
                            <button onClick={() => setShowingCheckinginList(false)}>Back to Tables</button>
                        </div>
                    ) : (
                        <>
                            <h1>Tables</h1>
                            {allTables.length > 0 ? (
                                <table className={"tableList"}>
                                    <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Latitude</th>
                                        <th>Longitude</th>
                                        <th>Checking Ins</th>
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
                                                <button onClick={() => CheckingInsListHandler(table.checkingIns)}>Checking Ins</button>
                                            </td>
                                            <td>
                                                <button onClick={() => console.log(`Pair Matches for table ID: ${table.id}`)}>Pair Matches</button>
                                            </td>
                                            <td>
                                                <button onClick={() => tableEditorHandler(table.id)}>Edit</button>
                                            </td>
                                            <td>
                                                <button onClick={() => deleteTableHandler(table.id)}>Delete</button>
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
            )}
        </>
    );

}

export default AdminTablesList;
