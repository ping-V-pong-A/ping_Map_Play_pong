import React, { useState } from 'react';
import AllTables from "./AllTables.jsx";
import NewTableForm from "./NewTableForm.jsx";

function Home() {
    const [showingAllTables, setShowingAllTables] = useState(false);
    const [addingNewTable, setAddingNewTable] = useState(false);

    const showAllTablesHandler = () => {
        setShowingAllTables(true);
    }

    const addNewTableHandler = () => {
        setAddingNewTable(true);
    }
    return (
        <>
            {showingAllTables ? (
                <AllTables />
            ) : addingNewTable ? (
                <NewTableForm />
            ) : (
                <div>
                    <h1>Find ping pong tables near you!</h1>
                    <button onClick={showAllTablesHandler}>Show me all the tables</button>
                    <button onClick={addNewTableHandler}>Add new tables</button>
                    <button>Logging in</button>
                </div>
            )}
        </>
    );
}

export default Home;