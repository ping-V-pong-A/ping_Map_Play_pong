import React, {useState} from "react";

export default function TableCreateForm(props) {
    const {
        onSave,
        onCancel
    } = props;

    const [name, setName] = useState("");
    const [latitude, setLatitude] = useState(0);
    const [longitude, setLongitude] = useState(0);
    
    const onSubmit = e => {
        e.preventDefault();
        
        return onSave({
            name,
            latitude,
            longitude
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
                    <label htmlFor="latitude">Latitude:</label>
                    <input 
                        value={latitude}
                        onChange={e => setLatitude(e.target.value)}
                        type="number"
                        name="latitude"
                        id="latitude"
                    />
                </div>
                <div>
                    <label htmlFor="longitude">Longitude:</label>
                    <input
                        value={longitude} onChange={e => setLongitude(e.target.value)}
                        type="number"
                        name="longitude"
                        id="longitude"
                    />
                </div>
                <button type="submit">Submit</button>
                <button type="button" onClick={onCancel}>Cancel</button>
            </form>
        </div>
    )
} 