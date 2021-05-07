import React from 'react';
import MyFlightTable from "../components/MyFlightTable"
import MyFlightForm from "../components/MyFlightForm"

import "../custom.css"

export const UpdateFlight = () => {
    return (
        <div>
            <h3 align="center">Список рейсов</h3>
            <MyFlightTable></MyFlightTable>
            <MyFlightForm></MyFlightForm>
        </div>
    );
}