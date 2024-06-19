import './Tables.scss'
import React, {useState, useEffect} from 'react';
import { useNavigate } from "react-router-dom";

import Map from "../../components/Map/Map.jsx";
import Loading from "../../components/Loading/Loading.jsx";
import AllTables from '../../components/TableList/TableList.jsx'
import TableList from "../../components/TableList/TableList.jsx";

const fetchAllTable = () => fetch('/api/Table')
    .then(resp => resp.json())

export default function Tables() {
    const navigate = useNavigate()
    const [loading, setLoading] = useState(true)
    const [tables, setTables] = useState(null)

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
    
    const props = {
        tables: tables
    }
    
    return (
        loading ? (
            <Loading/>
            ) : (
            <>
                <TableList {...props}/>
                <Map {...props}/>
                <button onClick={addNewTableHandler}>Add new table</button>
            </>
            )
    );
}