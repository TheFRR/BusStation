import React from 'react';
import Button from '@material-ui/core/Button';
import MyFlightTable from "../components/MyFlightTable"

import "../custom.css"

export const UpdateFlight = () => {
    return (
        <div>
            <h3 align="center">Список рейсов</h3>
            <MyFlightTable></MyFlightTable>
            <div style={{ width: '80%', margin: '10px auto' }}>              
                <Button color="primary" style={{ float: 'right' }}>Вернуться к списку маршрутов</Button>
            </div>
        </div>
    );
}