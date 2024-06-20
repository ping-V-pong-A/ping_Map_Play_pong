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
    
    const coordinates = [
        [48.1143376, 20.7816577],
        [48.1243376, 20.8016577],
        [48.1343376, 20.8116577]
    ]
    
    
    return (
            <MapContainer center={[48.1043376, 20.7916577]} zoom={12} scrollWheelZoom={false}>
                <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/#map=6/57.716/-22.324&layers=G">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                {coordinates.map(c => (
                    <Marker key={c} position={c}>
                        <Popup>
                            <button>
                                <Link to="/sign-in">Sign In</Link>
                            </button>
                            {c}
                        </Popup>
                    </Marker>))}
                {position &&
                    <Marker position={position}>
                        <Popup>You clicked here</Popup>
                    </Marker>
                }
               <LocationMarker setPosition={setPosition}/>
            </MapContainer>
        
    );
}