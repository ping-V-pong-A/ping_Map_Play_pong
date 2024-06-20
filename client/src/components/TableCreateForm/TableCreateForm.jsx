import React, {useState} from "react";

export default function TableCreateForm(props) {
    const {
        onSave,
        onCancel
    } = props;

    const [name, setName] = useState("");
    const [lat, setLat] = useState(0);
    const [lon, setLon] = useState(0);
    
    const onSubmit = e => {
        e.preventDefault();
        
        return onSave({
            name,
            lat,
            lon
        })
    };
    
    return (
        <div>
            <h1>Adding new table</h1>
            <form onSubmit={onSubmit}>
                <div>
                    <label htmlFor="name">Name:</label>
                    <input 
                        value={name}
                        onChange={e => setName(e.target.value)}
                        type="text"
                        name="ame"
                        id="name"
                    />
                </div>
                <div>
                    <label htmlFor="lat">Latitude:</label>
                    <input 
                        value={lat}
                        onChange={e => setLat(e.target.value)}
                        type="number"
                        name="lat"
                        id="lat"
                    />
                </div>
                <div>
                    <label htmlFor="lon">Longitude:</label>
                    <input
                        value={lon} onChange={e => setLon(e.target.value)}
                        type="number"
                        name="lon"
                        id="lon"
                    />
                </div>
                <button type="submit">Submit</button>
                <button type="button" onClick={onCancel}>Cancel</button>
            </form>
        </div>
    )
} 