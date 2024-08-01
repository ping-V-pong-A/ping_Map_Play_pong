import React, { useEffect, useState } from "react";

export default function AddMatch(props) {
    const [formData, setFormData] = useState({
        tableId: '',
        startDate: '',
        endDate: '',
        player1Id: '',
        player1Point: 0,
        player2Id: '',
        player2Point: 0,
    });

    const [players, setPlayers] = useState([]);
    const [tables, setTables] = useState([]);

 
    useEffect(() => {
        const fetchPlayers = async () => {
            try {
                const response = await fetch('/api/users');
                if (!response.ok) {
                    throw new Error('Failed to fetch players');
                }
                const data = await response.json();
                setPlayers(data);
            } catch (error) {
                console.error('Error fetching players:', error);
            }
        };

        fetchPlayers();
    }, []);


    useEffect(() => {
        const fetchTables = async () => {
            try {
                const response = await fetch('/api/tables');
                if (!response.ok) {
                    throw new Error('Failed to fetch tables');
                }
                const data = await response.json();
                setTables(data);
            } catch (error) {
                console.error('Error fetching tables:', error);
            }
        };

        fetchTables();
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        const matchRequest = {
            Id: 0,
            TableId: formData.tableId,
            StartDate: new Date(formData.startDate),
            EndDate: new Date(formData.endDate),
            Player1Id: parseInt(formData.player1Id, 10),
            Player1Point: parseInt(formData.player1Point, 10),
            Player2Id: parseInt(formData.player2Id, 10),
            Player2Point: parseInt(formData.player2Point, 10),
        };

        fetch('/api/matches/add', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(matchRequest),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to add match');
                }
                return response.text();
            })
            .then(data => {
                console.log('Match added:', data);
                props.onGoBack();
            })
            .catch(error => {
                console.error('Error adding match:', error);
            });
    };


    return (
        <div className="add-match-form">
            <h2>Add New Match</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>
                        Table ID:
                        <select
                            name="tableId"
                            value={formData.tableId}
                            onChange={handleChange}
                            required
                        >
                            <option value="">Select Table</option>
                            {tables.map(table => (
                                <option key={table.id} value={table.id}>
                                    {table.name}
                                </option>
                            ))}
                        </select>
                    </label>
                </div>
                <div>
                    <label>
                        Start Date:
                        <input
                            type="datetime-local"
                            name="startDate"
                            value={formData.startDate}
                            onChange={handleChange}
                            required
                        />
                    </label>
                </div>
                <div>
                    <label>
                        End Date:
                        <input
                            type="datetime-local"
                            name="endDate"
                            value={formData.endDate}
                            onChange={handleChange}
                            required
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Player 1 ID:
                        <select
                            name="player1Id"
                            value={formData.player1Id}
                            onChange={handleChange}
                            required
                        >
                            <option value="">Select Player 1</option>
                            {players.map(player => (
                                <option key={player.id} value={player.id}>
                                    {player.id}
                                </option>
                            ))}
                        </select>
                    </label>
                </div>
                <div>
                    <label>
                        Player 1 Points:
                        <input
                            type="number"
                            name="player1Point"
                            value={formData.player1Point}
                            onChange={handleChange}
                            required
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Player 2 ID:
                        <select
                            name="player2Id"
                            value={formData.player2Id}
                            onChange={handleChange}
                            required
                        >
                            <option value="">Select Player 2</option>
                            {players.map(player => (
                                <option key={player.id} value={player.id}>
                                    {player.id}
                                </option>
                            ))}
                        </select>
                    </label>
                </div>
                <div>
                    <label>
                        Player 2 Points:
                        <input
                            type="number"
                            name="player2Point"
                            value={formData.player2Point}
                            onChange={handleChange}
                            required
                        />
                    </label>
                </div>
                <button type="submit">Submit Match</button>
                <button type="button" onClick={() => setAddingNewMatch(false)}>Cancel</button>
            </form>
        </div>
    );
}
