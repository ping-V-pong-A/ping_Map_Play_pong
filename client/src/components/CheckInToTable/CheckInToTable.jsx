import React, { useState } from "react";

export default function CheckInToTable({ checkIn, setCheckIn, handleCheckIn, checkSwitch, setCheckSwitch }) {
    const [alertMessage, setAlertMessage] = useState("");

    const onSubmit = e => {
        e.preventDefault();

        if (!checkIn.start) {
            setAlertMessage("The start date is required.");
            return;
        }

        if (!checkIn.end) {
            setAlertMessage("The end date is required.");
            return;
        }

        if (new Date(checkIn.start).getTime() === new Date(checkIn.end).getTime()) {
            setAlertMessage("The start date and end date cannot be the same.");
            return;
        }

        const localStartDate = new Date(checkIn.start);
        const localEndDate = new Date(checkIn.end);


        const startDateUtc = new Date(localStartDate.getTime() - (localStartDate.getTimezoneOffset() * 60000)).toISOString();
        const endDateUtc = new Date(localEndDate.getTime() - (localEndDate.getTimezoneOffset() * 60000)).toISOString();

   const checkInRequest = {
            userId: checkIn.userId,
            tableId: checkIn.tableId,
            startDate: startDateUtc,
            endDate: endDateUtc,

        };

        console.log("CheckInRequest Object:", checkInRequest);
        handleCheckIn(checkInRequest);
    }

    const startDateChanger = (e) => {
        const selectedStartDate = new Date(e.target.value);
        const currentDateTime = new Date();
        const minValidStartDate = new Date(currentDateTime.getTime() + 30 * 60000);
        setAlertMessage("");
        if (selectedStartDate < minValidStartDate) {
            setAlertMessage("The start date must be at least 30 minutes in the future.");
        } else {
            setCheckIn({ ...checkIn, start: e.target.value });
            setAlertMessage("");
        }
    }

    const endDateChanger = (e) => {
        setAlertMessage("");
        setCheckIn({ ...checkIn, end: e.target.value });
    }

    return (
        <>
            <div>
                <h2>Check in to table #{checkIn.tableId}</h2>
            </div>
            <form onSubmit={onSubmit}>
                <div>
                    <label htmlFor="start">Start:</label>
                    <input
                        value={checkIn.start}
                        onChange={startDateChanger}
                        type="datetime-local"
                        name="start"
                        id="start"
                    />
                </div>
                <div>
                    <label htmlFor="end">End:</label>
                    <input
                        value={checkIn.end}
                        onChange={endDateChanger}
                        type="datetime-local"
                        name="end"
                        id="end"
                    />
                </div>
                <p>{alertMessage}</p>
                <button type="submit">Submit</button>
                <button type="button" onClick={() => setCheckSwitch({ ...checkSwitch, switch: !checkSwitch.switch })}>
                    Cancel
                </button>
            </form>
        </>
    );
}
