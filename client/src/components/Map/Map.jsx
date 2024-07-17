import "leaflet/dist/leaflet.css";
import React, {useState} from "react";
import { MapContainer, Marker, Popup, TileLayer, useMapEvents } from "react-leaflet";
import {Link} from "react-router-dom";
import CheckInToTable from "../CheckInToTable/CheckInToTable.jsx";

function UserLocationMarker() {
    const [position, setPosition] = useState(null)
    const map = useMapEvents({
        click() {
            map.locate()
        },
        locationfound(e) {
            setPosition(e.latlng)
            map.flyTo(e.latlng, map.getZoom())
        },
    })

    return position === null ? null : (
        <Marker position={position}>
            <Popup>You are here</Popup>
        </Marker>
    )
}

export default function Map({tables, profile, checkIn, setCheckIn, handleCheckIn}) {
    const [position, setPosition] = useState(null);
    const [checkSwitch, setCheckSwitch] = useState(false);
    
    const props = {
        checkIn,
        setCheckIn,
        handleCheckIn
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
                            <button onClick={e=> {
                                setCheckIn({...checkIn, tableId: t.id})
                                setCheckSwitch(!checkSwitch)}}
                            >
                                {checkSwitch ? "Cancel" : "Check-In" }                               
                            </button>
                            {checkSwitch && <CheckInToTable{...props}/>}
                        </Popup>
                    </Marker>
                ))}
                <UserLocationMarker/>
            </MapContainer>

    );
}