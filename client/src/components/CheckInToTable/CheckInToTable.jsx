import React from "react";

export default function CheckInToTable({ checkIn, setCheckIn, handleCheckIn, checkSwitch, setCheckSwitch}) {

    const onSubmit = e => {
        e.preventDefault();
        console.log(checkIn)
        return handleCheckIn(checkIn)
    }
    
    return (
        <>
            <form onSubmit={onSubmit}>
                <div>
                    <label htmlFor="start">Start:</label>
                    <input
                        value={checkIn.start} onChange={e => setCheckIn({...checkIn, start: e.target.value})}
                        type="datetime-local"
                        name="start"
                        id="strat"
                    />
                </div>
                <div>
                    <label htmlFor="end">End:</label>
                    <input
                        value={checkIn.end} onChange={e => setCheckIn({...checkIn, end: e.target.value})}
                        type="datetime-local"
                        name="end"
                        id="end"
                    />
                </div>
                <button type="submit">Submit</button>
                <button onClick={_ => {
                    setCheckSwitch({...checkSwitch, switch:!checkSwitch.switch});
                    console.log(checkIn)
                }}>Cancel</button>
            </form>
        </>
    )
}