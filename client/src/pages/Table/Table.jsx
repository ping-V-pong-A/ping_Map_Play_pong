import { useParams } from "react-router-dom";
import React, { useEffect, useState } from "react";
import Loading from "../../components/Loading/Loading.jsx";
import { useNavigate } from "react-router-dom";

const getTable = (tableId) => fetch(`/api/tables/${tableId}`)
    .then(resp => resp.json())
    .catch(err => console.error(err));

export default function Table() {
    const { id } = useParams();
    const navigate = useNavigate();
    const [table, setTable] = useState(null);

    useEffect(() => {
        getTable(id).then(data => setTable(data));
    }, [id]);


    const formatDate = (dateString) => {
        const options = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', hour12: false };
        return new Date(dateString).toLocaleString('hu-HU', options).replace(',', ''); // Cseréljük le a vesszőt
    };
    
 
    return (
        <>
            {table ? (
                <div>
                 
                    <ul>
                        <li>
                            <table>
                                <thead>
                                <tr>
                                    <th>Table Name</th>
                                    <th>User Id</th>
                                    <th>Start</th>
                                    <th>End</th>
                                </tr>
                                </thead>
                                <tbody>
                                {table.checkingIns.map(checkIn => (
                                    <tr key={checkIn.id}>
                                        <td>{table.name}</td>
                                        <td>{checkIn.userId}</td>
                                        <td>{formatDate(checkIn.startDate)}</td>
                                        <td>{formatDate(checkIn.endDate)}</td>
                                    </tr>
                                ))}
                                </tbody>
                            </table>
                        </li>
                    </ul>
                    <button onClick={() => navigate('/tables')}>Go back</button>
                </div>
            ) : (
                <Loading />
            )}
        </>
    );
}
