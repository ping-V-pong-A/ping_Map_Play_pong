import './Tables.scss';
import React, { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import { useProfile } from "../../contexts/ProfileContext.jsx";

import Map from "../../components/Map/Map.jsx";
import Loading from "../../components/Loading/Loading.jsx";
import TableList from "../../components/TableList/TableList.jsx";

const fetchAllTable = () => fetch('/api/tables').then(resp => resp.json());

const postCheckIn = (checkIn) => fetch('/api/check-ins/add', {
    method: "POST",
    headers: {
        "Content-Type": "application/json"
    },
    credentials: 'include',
    body: JSON.stringify(checkIn)
})
    .then((response) => {
        if (response.ok) {
            const contentType = response.headers.get("content-type");
            if (contentType && contentType.indexOf("application/json") !== -1) {
                return response.json().then(data => {
                    console.log("Success:", data);
                });
            } else {
                return response.text().then(text => {
                    console.log("Success:", text);
                });
            }
        } else {
            console.error(`Error: ${response.status} ${response.statusText}`);
        }
    })
    .catch((error) => console.error('Error:', error));

export default function Tables() {
    const navigate = useNavigate();
    const { profile } = useProfile();
    const [loading, setLoading] = useState(true);
    const [tables, setTables] = useState(null);
    const [listMapSwitch, setListMapSwitch] = useState(true);

    const [checkIn, setCheckIn] = useState({
        userId: profile ? profile.id : 1,
        tableId: 0,
        startDate: "",
        endDate: ""
    });

    useEffect(() => {
        fetchAllTable().then(tables => {
            setLoading(false);
            setTables(tables);
        });
    }, []);

    const addNewTableHandler = () => {
        navigate('/tables/new');
    };

    const handleCheckIn = (checkIn) => {
        
        return postCheckIn(checkIn).then(() => navigate("/tables"));
    };

    const props = {
        tables,
        profile,
        checkIn,
        setCheckIn,
        handleCheckIn
    };

    return (
        loading ? (
            <Loading />
        ) : (
            <>
                <div className={"buttons"}>
                    <button onClick={() => setListMapSwitch(!listMapSwitch)}>
                        {listMapSwitch ? "Map" : "List"}
                    </button>
                    <button onClick={addNewTableHandler}>Add new table</button>
                </div>

                {listMapSwitch ?
                    <TableList {...props} />
                    :
                    <Map {...props} />}
            </>
        )
    );
}
