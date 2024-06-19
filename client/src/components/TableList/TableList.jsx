import React, { useState, useEffect } from 'react';

export default function AllTables({tables}) {

    return (
        <>
            <h1>Tables</h1>
            <ul>
                {tables && tables.map(table => (
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
