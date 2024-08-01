import React, { useState } from 'react';
import CheckInToTable from "../CheckInToTable/CheckInToTable.jsx";
import { useNavigate } from "react-router-dom";

export default function TableList({ tables, checkIn, setCheckIn, handleCheckIn }) {
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
            {!checkSwitch.switch ? (
                <CheckInToTable {...props} />
            ) : (
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
                                <button onClick={() => {
                                    setCheckIn(prevCheckIn => ({
                                        ...prevCheckIn,
                                        tableId: table.id,
                                        start: "",
                                        end: "" 
                                    }));
                                    setCheckSwitch(prevState => ({
                                        ...prevState,
                                        id: table.id,
                                        switch: !prevState.switch
                                    }));
                                }}>Check In</button>
                            </td>
                            <td>
                                <button onClick={() => navigate(`table/${table.id}`)}>Details</button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            )}
        </>
    );
}
