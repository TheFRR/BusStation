import React from 'react';
import MyRoutesTable from "../components/MyRoutesTable"
import MyRouteForm from "../components/MyRouteForm"
import "../custom.css"

export const UpdateRoute = () => {
  return (
    <div>
      <h3 align="center">Список маршрутов</h3>
      <MyRoutesTable></MyRoutesTable>
      <h3 align="center" style={{marginTop: '30px'}}>Создание маршрута</h3>
      <div align="center"><MyRouteForm></MyRouteForm></div>
    </div>
  );
}