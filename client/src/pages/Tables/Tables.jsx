import './Tables.scss'
import React, {useState, useEffect} from 'react';
import { useNavigate } from "react-router-dom";
import {useProfile} from "../../contexts/ProfileContext.jsx";

import SessionTimeout from "../../components/sessionTimeout/sessionTimeout.js";
import Map from "../../components/Map/Map.jsx";
import Loading from "../../components/Loading/Loading.jsx";
import TableList from "../../components/TableList/TableList.jsx";

const fetchAllTable = () => fetch('/api/Table')
    .then(resp => resp.json())

const postCheckIn = (checkIn) => fetch('/api/CheckingIn/checkingIns/add', {
    method: "POST",
    headers: {
        "Content-Type": "application/json"
    },
    credentials: 'include',
    body: JSON.stringify(checkIn)
})
    .then((res) => res.json())
    .catch((error) => console.error('Error:', error))

export default function Tables() {
    
    SessionTimeout();
    const navigate = useNavigate()
    const {profile} = useProfile();
    const [loading, setLoading] = useState(true)
    const [tables, setTables] = useState(null)
    const [listMapSwitch, setListMapSwitch] = useState(true)

    useEffect(() => {
        fetchAllTable()
            .then(tables => {
                setLoading(false)
                setTables(tables)
            })
    }, []);

    const addNewTableHandler = () =>{
        navigate('/tables/new')
    }
    
   const handleCheckIn = (checkIn) => postCheckIn(checkIn).then(navigate("/tables"))

    const props = {
        tables: tables,
        profile: profile,
        handleCheckIn: handleCheckIn
    }

    return (
        loading ? (
            <Loading/>
        ) : (
            <>
                <button onClick={_ => setListMapSwitch(!listMapSwitch)}>List / Map</button>
                <button onClick={addNewTableHandler}>Add new table</button>
                {listMapSwitch ?
                <TableList {...props}/>                    
                :
                <Map {...props}/>                }
            </>
        )
    );
}