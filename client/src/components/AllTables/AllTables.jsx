import React, { useState, useEffect } from 'react';

function AllTables() {
    const [allTables, setAllTables] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchTables = async () => {
            try {
                const response = await fetch('/api/Table');
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const data = await response.json();
                setAllTables(data);
            } catch (error) {
                console.error('Error fetching tables:', error);
                setError(error);
            }
        };

        fetchTables();
    }, []);

    return (
        <>
            <h1>Tables</h1>
            <ul>
                {allTables.map(table => (
                    <li key={table.id}>
                        <p>Name: {table.name}</p>
                        {table.coordinate && (
                            <>
                                <p>Latitude: {table.coordinate.lat}</p>
                                <p>Longitude: {table.coordinate.lon}</p>
                            </>
                        )}
                    </li>
                ))}
            </ul>
        </>
    );
}

export default AllTables;
