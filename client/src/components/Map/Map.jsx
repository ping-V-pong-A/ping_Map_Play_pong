import "leaflet/dist/leaflet.css";
import React, {useState} from "react";
import { MapContainer, Marker, Popup, TileLayer, useMapEvents } from "react-leaflet";
import {Link} from "react-router-dom";

function LocationMarker({ setPosition }) {
    useMapEvents({
        click(e) {
            setPosition(e.latlng);
        },
    });
    return null;
}

export default function Map({tables, profile, handleCheckIn}) {
    const [position, setPosition] = useState(null);
    const [checkSwitch, setCheckSwitch] = useState(false)
    const [checkIn, setCheckIn] = useState({
        userId: profile ? profile.id : "",
        tableId: 0,
        start: "",
        end: ""
    })
    
    const onSubmit = e => {
        e.preventDefault();
        console.log(checkIn)
        return handleCheckIn(checkIn)
    }
    
    return (
            <MapContainer center={[48.1043376, 20.7916577]} zoom={12} scrollWheelZoom={false}>
                <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/#map=6/57.716/-22.324&layers=G">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                {tables.map(t => (
                    <Marker key={t.id} position={[t.lat, t.lon]}>
                        <Popup>
                            <button onClick={_=> {
                                setCheckIn({...checkIn, tableId: t.id})
                                setCheckSwitch(!checkSwitch)}
                            }>
                            Check-In    
                            </button>
                            {checkSwitch &&
                                <form onSubmit={onSubmit}>
                                    <div>
                                        <label htmlFor="start">Start:</label>
                                        <input
                                            value={checkIn.start} onChange={e => setCheckIn({...checkIn, start: e.target.value})}
                                            type="datetime-local"
                                            name="start"
                                            id="strat"
                                        />
                                    </div>
                                    <div>
                                        <label htmlFor="end">End:</label>
                                        <input
                                            value={checkIn.end} onChange={e => setCheckIn({...checkIn, end: e.target.value})}
                                            type="datetime-local"
                                            name="end"
                                            id="end"
                                        />
                                    </div>
                                    <button type="submit">Submit</button>
                                </form>}
                        </Popup>
                    </Marker>
                ))}
            </MapContainer>

    );
}