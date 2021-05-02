import React from 'react';
import MyRoutesTable from "../components/MyRoutesTable.jsx"
import MyRouteForm from "../components/MyRouteForm.jsx"
import MyButtonForTable from "../components/MyButtonsForTable.jsx"
import "../custom.css"

export const AdminMain = () => {
  return (
    <div>
      <h3 align="center">Список маршрутов</h3>
      <MyRoutesTable></MyRoutesTable>
      <MyButtonForTable></MyButtonForTable>
      <h3 align="center">Создание маршрута</h3>
      <div align="center"><MyRouteForm></MyRouteForm></div>
    </div>
  );
}