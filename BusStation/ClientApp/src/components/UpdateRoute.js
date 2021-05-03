import React from 'react';
import MyRoutesTable from "../components/MyRoutesTable"
import MyRouteForm from "../components/MyRouteForm"
import Button from '@material-ui/core/Button';
import "../custom.css"

export const UpdateRoute = () => {
  return (
    <div>
      <h3 align="center">Список маршрутов</h3>
      <MyRoutesTable></MyRoutesTable>
      <div style={{ width: '80%', margin: '10px auto' }}>
        <Button color="primary">Редактировать</Button>
        <Button color="primary">Удалить</Button>
      </div>
      <h3 align="center">Создание маршрута</h3>
      <div align="center"><MyRouteForm></MyRouteForm></div>
    </div>
  );
}