import {useParams} from "react-router-dom";
import React, {useEffect, useState} from "react";
import Loading from "../../components/Loading/Loading.jsx";

const getTable = (tableId) => fetch(`/api/Table/tables/id/${tableId}`)
    .then(resp => resp.json())
    .catch(err => console.error(err))

export default function Table() {
    const {id} = useParams()
    const [table, setTable] = useState()

    useEffect(() => {
        getTable(id).then(data => setTable(data))
    }, []);
    
    return (
        <>
            {table ? (
                <div>
                    <h1 onClick={_ => console.log(table)}>{table.id}</h1>
                    <ul>
                        <li>
                            <table>
                                <thead>
                                    <tr>                                          
                                        <th><li>Table Name</li></th>                                        
                                        <th><li>User Id</li></th>
                                        <th><li>Start</li></th>
                                        <th><li>End</li></th>
                                    </tr>
                                </thead>
                            </table>
                        </li>
                        {table.checkingIns.map(checkIn => (
                        <li key={checkIn.id}>
                            <table>
                                <tbody>
                                    <tr>
                                        <th>
                                            <li>{table.name}</li>
                                        </th>
                                        <th>
                                            <li>{checkIn.userId}</li>
                                        </th>
                                        <th>
                                            <li>{checkIn.startDate}</li>
                                        </th>
                                        <th>
                                            <li>{checkIn.endDate}</li>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                        </li>                                            
                        ))}
                    </ul>
                </div>
            ) : <Loading/>}
        </>
    )
}