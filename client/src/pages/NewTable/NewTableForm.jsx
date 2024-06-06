import React, { useState } from 'react';
import './NewTableForm.scss';

function NewTableForm() {

    const [name, setName] = useState("");
    const [latitude, setLatitude] = useState("");
    const [longitude, setLongitude] = useState("");

    const onSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('/api/tables', {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    name: name,
                    latitude: latitude,
                    longitude: longitude,
                })
            });
            const data = await response.json();
            if (response.ok) {
                console.log("Table created successfully");
            } else {
                console.log("Error while creating table:", data.message);
            }
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div>
            <h1>Adding new table</h1>
            <form onSubmit={onSubmit}>
                <div>
                    <label htmlFor="name">Name:</label>
                    <input value={name} onChange={(e) => setName(e.target.value)} type="text" id="name" name="name" />
                </div>
                <div>
                    <label htmlFor="latitude">Latitude:</label>
                    <input value={latitude} onChange={(e) => setLatitude(e.target.value)} type="text" id="latitude" name="latitude" />
                </div>
                <div>
                    <label htmlFor="longitude">Longitude:</label>
                    <input value={longitude} onChange={(e) => setLongitude(e.target.value)} type="text" id="longitude" name="longitude" />
                </div>
                <button type="submit">Submit</button>
            </form>
        </div>
    );
}

export default NewTableForm;
