import "leaflet/dist/leaflet.css";
import React, {useState} from "react";
import { MapContainer, Marker, Popup, TileLayer, useMapEvents } from "react-leaflet";

function LocationMarker({ setPosition }) {
    useMapEvents({
        click(e) {
            setPosition(e.latlng);
        },
    });
    return null;
}

export default function Map() {
    const [position, setPosition] = useState(null);
    
    return (
            <MapContainer center={[51.505, -0.09]} zoom={13} scrollWheelZoom={false}>
                <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                {position &&
                    <Marker position={position}>
                        <Popup>You clicked here</Popup>
                    </Marker>
                }
                <LocationMarker setPosition={setPosition}/>
            </MapContainer>
        
    );
}