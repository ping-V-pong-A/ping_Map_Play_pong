import React, {useState, useEffect} from 'react';

import {useProfile} from "../../contexts/ProfileContext.jsx";
import AdminUsersList from '../../components/AdminUsersList/AdminUsersList.jsx';

import { useNavigate } from "react-router-dom";


import AdminTablesList from "../../components/AdminTablesList/AdminTablesList.jsx";
export default function AdminPage() {

    const [chosenTask, setChosenTask] = useState("");
    
    
    const listAllUsersHandler = () =>{
        setChosenTask("usersList");
    }

    const listAllTablesHandler = () =>{
        setChosenTask("tablesList");
    }




    const saveDataHandler = () =>{
        setChosenTask("");
    }

    return (
        <>
            {chosenTask === "" ? (
                <ul><h1>Welcome, Admin!<br/>
                    What would you like to do today?</h1>
                    <button onClick={listAllUsersHandler}>List all the users</button>
                    <button onClick={listAllTablesHandler}>List all the tables</button>
                    <button>Search a table by id or name</button>
                </ul>
            ) : (
                <>
                    {chosenTask === "usersList" && <AdminUsersList onSaveData={saveDataHandler}/>}
                    {chosenTask === "tablesList" && <AdminTablesList onSaveData={saveDataHandler}/>}
                </>
            )}
        </>
    );

}