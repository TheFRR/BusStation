import React, { useState, useEffect } from 'react';
import { DataGrid } from '@material-ui/data-grid';
import { getRoutes } from '../API/routesApi'

const columns = [
  { field: 'number', headerName: 'Номер маршрута', width: 180, type: 'string' },
  { field: 'arrival', headerName: 'Пункт отправки', width: 180, type: 'string' },
  { field: 'departure', headerName: 'Пункт прибытия', width: 180, type: 'string' },
];

export default function DataTable() {
  const [routes, setRoutes] = useState([]);
  const myMethod = async () => {
    let temp = await getRoutes();
    setRoutes(temp);
  };
  useEffect(() => {
    myMethod();
  }, []);
  let rows = [];
  routes.map(r => rows.push({ id: r.id, number: r.number, arrival: r.arrival, departure: r.departure }));
  return (
    <div style={{ height: 380, width: '80%', margin: 'auto' }}>
      <DataGrid rows={rows} columns={columns} pageSize={5} checkboxSelection></DataGrid>
    </div>
  );
}