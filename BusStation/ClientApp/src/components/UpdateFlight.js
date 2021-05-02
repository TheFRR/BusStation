import React from 'react';
import Button from '@material-ui/core/Button';
import MyFlightTable from "./MyFlightTable"

import "../custom.css"

export const UpdateFlight = () => {
    return (
        <div>
            <h3 align="center">Список рейсов</h3>
            <MyFlightTable></MyFlightTable>
            <div style={{ width: '80%', margin: '10px auto' }}>
                <Button color="primary">Редактировать</Button>
                <Button color="primary">Удалить</Button>
                <Button color="primary" style={{ float: 'right' }}>Вернуться к списку маршрутов</Button>
            </div>
        </div>
    );
}