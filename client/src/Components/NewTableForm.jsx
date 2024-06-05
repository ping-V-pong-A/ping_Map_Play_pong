import React from 'react';

function NewTableForm() {
    return (
        <div>
            <h1>Adding new table</h1>
            <form>
                <div>
                    <label htmlFor="name">Name:</label>
                    <input type="text" id="name" name="name" />
                </div>
                <div>
                    <label htmlFor="latitude">Latitude:</label>
                    <input type="text" id="latitude" name="latitude" />
                </div>
                <div>
                    <label htmlFor="longitude">Longitude:</label>
                    <input type="text" id="longitude" name="longitude" />
                </div>
                <button type="submit">Submit</button>
            </form>
        </div>
    );
}

export default NewTableForm;