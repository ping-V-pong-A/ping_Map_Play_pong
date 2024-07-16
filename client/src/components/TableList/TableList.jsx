import React, {useState} from 'react';
import {Popup} from "react-leaflet";
import CheckInToTable from "../CheckInToTable/CheckInToTable.jsx";

export default function TableList({tables,  checkIn, setCheckIn,  handleCheckIn}) {
    const [checkSwitch, setCheckSwitch] = useState({
        id: null,
        switch: false
    });
    
    const props = {
        handleCheckIn,
        checkIn,
        setCheckIn,
    }

    return (
        <>
            {
                checkSwitch.switch && (
                    <Popup>
                        <CheckInToTable {...props}/>
                    </Popup>
                )
            }
                        <h1>Tables</h1>                        
            <table>
                <thead>
                    <tr>
                        <th><li>Id</li></th>
                        <th><li>Name</li></th>
                        <th><li>CheckIn</li></th>
                        <th><li></li></th>                        
                    </tr>
                </thead>
                <tbody>                 
                    {tables && tables.map(table => (                  
                        <tr key={table.id}>                            
                            <td><li>{table.id}</li></td>
                            <td><li>{table.name}</li></td>
                            <td>
                                <button onClick={_ => (
                                    setCheckSwitch({...checkSwitch, id: table.id, switch: !checkSwitch.switch})                                    
                                )}>
                                    checkIn
                                </button>
                            </td>
                            <td><button>details</button></td>
                        </tr> 
                    ))}
                </tbody>
            </table>
        </>
    );
}
