import React, { useState, useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { DataGrid } from '@material-ui/data-grid';
import { getFlights } from '../API/FlightsApi';
import { deleteFlight } from '../API/FlightsApi';

const columns = [
  { field: 'routeNumber', headerName: 'Маршрут', width: 200, type: 'string' },
  { field: 'departureTime', headerName: 'Время отправления', width: 200, type: 'dateTime' },
  { field: 'arrivalTime', headerName: 'Время прибытия', width: 200, type: 'dateTime' },
  { field: 'busySeats', headerName: 'Занято мест', width: 200, type: 'string' },
  { field: 'seats', headerName: 'Всего мест', width: 200, type: 'string' }
];

export default function DataTable() {
  const [flights, setFlights] = useState([]);
  const myMethod = async () => {
    let temp = await getFlights();
    setFlights(temp);
  };

  useEffect(() => {
    myMethod();
  }, []);

  const [deletedRows, setDeletedRows] = useState([]);

  const handleRowSelection = (e) => {
    setDeletedRows([]);
    setDeletedRows([...deletedRows, ...rows.filter((r) => r.id === e.data.id)]);
    console.log(deletedRows);
  };

  const handlePurge = () => {
    deleteFlight(deletedRows[deletedRows.length - 1].id);
    window.location.reload();
  };

  let rows = [];
  console.log(flights);
  flights.map(f => rows.push({
    id: f.id, routeNumber: f.route.number, arrivalTime: new Date(f.arrivalTime), departureTime: new Date(f.departureTime),
    busySeats: f.busySeatsNumber, seats: f.seatsNumber
  }));

  return (
    <div style={{ height: 380, width: '80%', margin: 'auto' }}>
      <DataGrid rows={rows} columns={columns} pageSize={5} 
      onRowSelected={handleRowSelection}></DataGrid>
      <div style={{ width: '80%', marginTop: '5px' }}>
        <Button color="primary">Редактировать</Button>
        <Button color="primary" onClick={handlePurge}>Удалить</Button>
      </div>
    </div>
  );
}