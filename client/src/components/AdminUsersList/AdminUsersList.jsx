import React, {useState} from 'react';
import CheckInToTable from "../CheckInToTable/CheckInToTable.jsx";
import {useNavigate} from "react-router-dom";

export default function TableList({tables, checkIn, setCheckIn, handleCheckIn}) {
    const navigate = useNavigate();
    const [checkSwitch, setCheckSwitch] = useState({
        id: null,
        switch: true
    });

    const props = {
        handleCheckIn,
        checkIn,
        setCheckIn,
        checkSwitch,
        setCheckSwitch
    }

    return (
        <>
            {!checkSwitch.switch ? <CheckInToTable{...props}/> : (
                <table className={"tableList"}>
                    <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>CheckIn</th>
                        <th></th>
                    </tr>
                    </thead>
                    <tbody>

                    {tables && tables.map(table => (
                        <tr key={table.id}>
                            <td>{table.id}</td>
                                <td>{table.name}</td>
                                <td>
                                    <button onClick={_ => {
                                        setCheckIn({...checkIn, tableId: table.id});
                                        setCheckSwitch({...checkSwitch, id: table.id, switch: !checkSwitch.switch})
                                    }}>checkIn
                                    </button>
                                </td>
                                <td>
                                    <button onClick={_ => navigate(`table/${table.id}`)}>details</button>
                                </td>
                        </tr>                    

                        ))}
                    </tbody>
                </table>
                    )}
                    </>
                    );
                    }