import * as React from 'react';
import { DataGrid } from '@material-ui/data-grid';

const columns = [
  { field: 'id', headerName: 'ID', width: 70 },
  { field: 'route', headerName: 'Маршрут', width: 200, editable: false },
  { field: 'departureTime', headerName: 'Время отправления', width: 200, editable: false },
  { field: 'arrivalTime', headerName: 'Время прибытия', width: 200, editable: false },
  { field: 'busySeats', headerName: 'Занято мест', width: 200, editable: false },
  { field: 'seats', headerName: 'Всего мест', width: 200, editable: false }
];

const rows = [
  { id: 1, lastName: 'Snow', firstName: 'Jon', age: 35 },
  { id: 2, lastName: 'Lannister', firstName: 'Cersei', age: 42 },
  { id: 3, lastName: 'Lannister', firstName: 'Jaime', age: 45 },
  { id: 4, lastName: 'Stark', firstName: 'Arya', age: 16 },
  { id: 5, lastName: 'Targaryen', firstName: 'Daenerys', age: null }
];

export default function DataTable() {
  return (
    <div style={{ height: 380, width: '80%', margin: 'auto' }}>
      <DataGrid rows={rows} columns={columns} pageSize={5} checkboxSelection ></DataGrid>
    </div>
  );
}