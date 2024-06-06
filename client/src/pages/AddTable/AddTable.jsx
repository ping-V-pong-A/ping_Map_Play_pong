import React from 'react';
import { useNavigate } from "react-router-dom";
import './AddTable.scss';

import TableCreateForm from "../../components/TableCreateForm/TableCreateForm.jsx";

const postTable = (table) => {
    return fetch(`/api/Table/tables/add`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(table)
    })
        .then((res) => {
            if (!res.ok) {
                throw new Error(`HTTP error! status: ${res.status}`);
            }
            return res.json();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

export default function AddTable() {
    const navigate = useNavigate();
    
    const handleCreateTable = (table) => {
        postTable(table)
            .then(_ => navigate("/"))
    }
    
    const props = {
        onSave: handleCreateTable,
        onCancel: _ => navigate("/")        
    }    

    return (
        <>
            <TableCreateForm {...props}/>
        </>
    );
}
