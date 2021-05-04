import React, { useState, useEffect } from 'react';
import { DataGrid } from '@material-ui/data-grid';
import { getRoutes } from '../API/routesApi'
import Button from '@material-ui/core/Button';
import { deleteRoute } from '../API/routesApi'

const columns = [
  { field: 'number', headerName: 'Номер маршрута', width: 180, type: 'string' },
  { field: 'departure', headerName: 'Пункт отправки', width: 180, type: 'string' },
  { field: 'arrival', headerName: 'Пункт прибытия', width: 180, type: 'string' },
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

  const [deletedRows, setDeletedRows] = useState([]);

  const handleRowSelection = (e) => {
    setDeletedRows([...deletedRows, ...rows.filter((r) => r.id === e.data.id)]);
    console.log(deletedRows);
  };

  const handlePurge = () => {
      //rows.filter((r) => deletedRows.filter((sr) => sr.id === r.id).length < 1);
      deletedRows.map(r => {console.log(Number(r.id)); deleteRoute(r.id)});
      window.location.reload();
  };
  
  return (
    <div style={{ height: 380, width: '80%', margin: 'auto' }}>
      <DataGrid rows={rows} columns={columns} pageSize={5} checkboxSelection
        onRowSelected={handleRowSelection}></DataGrid>
      <div style={{ width: '80%', marginTop: '5px' }}>
        <Button color="primary">Редактировать</Button>
        <Button color="primary" onClick={handlePurge}>Удалить</Button>
      </div>
    </div>
  );
}