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

export default function Map({tables}) {
    const [position, setPosition] = useState(null);
    const [checkSwitch, setCheckSwitch] = useState(false)
  
    
    return (
            <MapContainer center={[48.1043376, 20.7916577]} zoom={12} scrollWheelZoom={false}>
                <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/#map=6/57.716/-22.324&layers=G">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                {tables.map(t => (
                    <Marker key={t.id} position={[t.lat, t.lon]}>
                        <Popup>
                            <button>
                                <Link to="/sign-in">Sign In</Link>
                            </button>
                            <button onClick={_=> setCheckSwitch(!checkSwitch)}>
                                
                            </button>
                        </Popup>
                    </Marker>
                ))}
            </MapContainer>

    );
}