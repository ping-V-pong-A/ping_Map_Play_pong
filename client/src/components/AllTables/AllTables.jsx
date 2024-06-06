import React, {useState, useEffect} from 'react';


const [allTables, setAllTables] = useState([])

useEffect(() => {
    fetch('api/tables')
        .then(response => response.json())
        .then(data => setAllTables(data))
        .catch(error => console.error('Error fetching tables:', error));
}, []);

function AllTables() {

return (
    <>
        <h1>Tables</h1>
        <ul>
            {allTables.map(table => (
                <li key={table.name}>
                    <p>Latitude: {table.latitude}</p>
                    <p>Longitude: {table.longitude}</p>
                </li>
            ))}
        </ul>
    </>
);
}


export default AllTables