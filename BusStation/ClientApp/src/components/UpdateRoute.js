import React from 'react';
import MyRoutesTable from "../components/MyRoutesTable"
import MyRouteForm from "../components/MyRouteForm"
import "../custom.css"

export const UpdateRoute = () => {
  return (
    <div>
      <h3 align="center">Список маршрутов</h3>
      <div><MyRoutesTable></MyRoutesTable></div>
      <div align="center" style={{margin: '30px'}}><MyRouteForm></MyRouteForm></div>
    </div>
  );
}