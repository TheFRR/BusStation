import React from 'react';
import Button from '@material-ui/core/Button';
import MyFlightTable from "../components/MyFlightTable"
import MyFlightForm from "../components/MyFlightForm"

import "../custom.css"

export const UpdateFlight = () => {
    return (
        <div>
            <h3 align="center">Список рейсов</h3>
            <MyFlightTable></MyFlightTable>
            <div style={{ width: '80%', margin: '10px auto' }}>
                
            </div>
            <div align="center" style={{ margin: '30px auto' }}>
                <h3 align="center">Добавление рейса</h3>
                <div align="center"><MyFlightForm></MyFlightForm></div>
            </div>
        </div>
    );
}