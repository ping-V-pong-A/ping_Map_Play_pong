import React, {useState, useEffect} from 'react';
import AllTables from '../../components/AllTables/AllTables.jsx'
import { useNavigate } from "react-router-dom";
import './Tables.scss'
function Tables() {

    const navigate = useNavigate()
    const addNewTableHandler = () =>{
        navigate('/tables/new')
    }

   
    
    return (<>
        <AllTables/>
        <button onClick={addNewTableHandler}>Add new table</button>
        </>
    );
}

export default Tables;