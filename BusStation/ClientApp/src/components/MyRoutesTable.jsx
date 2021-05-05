import React, { useState, useEffect } from 'react';
import { DataGrid } from '@material-ui/data-grid';
import { getRoutes } from '../API/RoutesApi'
import Button from '@material-ui/core/Button';
import Input from '@material-ui/core/Input';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { deleteRoute } from '../API/RoutesApi';
import { updateRoute } from '../API/RoutesApi';

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
    setDeletedRows([]);
    setDeletedRows([...deletedRows, ...rows.filter((r) => r.id === e.data.id)]);
    console.log(deletedRows);
  };

  const handlePurge = () => {
    //deletedRows.map(r => { deleteRoute(r.id) });
    deleteRoute(deletedRows[deletedRows.length - 1].id);
    window.location.reload();
  };

  const [open, setOpen] = React.useState(false);

  const [id, setId] = React.useState('');
  const [number, setNumber] = React.useState('');
  const [departure, setDeparture] = React.useState('');
  const [arrival, setArrival] = React.useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    updateRoute( { id, number, departure, arrival });
    window.location.reload();
  };

  const handleClickOpen = () => {
    setOpen(true);
    setId(deletedRows[deletedRows.length - 1].id);
    setNumber(deletedRows[deletedRows.length - 1].number);
    setDeparture(deletedRows[deletedRows.length - 1].departure);
    setArrival(deletedRows[deletedRows.length - 1].arrival);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div style={{ height: 380, width: '80%', margin: 'auto' }}>
      <DataGrid rows={rows} columns={columns} pageSize={5}
        onRowSelected={handleRowSelection}></DataGrid>
      <div style={{ width: '80%', marginTop: '5px' }}>
        <Button color="primary" onClick={handleClickOpen}>Редактировать</Button>
        <Button color="primary" onClick={handlePurge}>Удалить</Button>
      </div>

      <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
        <DialogTitle align="center">Редактирование маршрута</DialogTitle>
        <DialogContentText align="center" style={{ paddingLeft: '10px', paddingRight: '10px' }}>
            Let Google help apps determine location. This means sending anonymous location data to
            Google, even when no apps are running.
          </DialogContentText>
        <DialogContent>
          <form onSubmit={handleSubmit}>
            <div style={{ marginBottom: '15px' }}>
              <TextField
                id="number"
                type="number"
                InputProps={{ inputProps: { min: 1 } }}
                label="Номер маршрута"
                fullWidth
                variant="outlined"
                value={number}
                onChange={(e) => setNumber(e.target.value)}
              />
            </div>
            <div style={{ marginBottom: '15px' }}>
              <TextField
                id="departure"
                label="Пункт отправки"
                fullWidth
                variant="outlined"
                value={departure}
                onChange={(e) => setDeparture(e.target.value)}
              />
            </div>
            <div style={{ marginBottom: '15px' }}>
              <TextField
                id="arrival"
                label="Пункт прибытия"
                fullWidth
                variant="outlined"
                value={arrival}
                onChange={(e) => setArrival(e.target.value)}
              />
            </div>
            <div style={{ margin: '25px auto' }}>
              <Input fullWidth type="submit" value="Сохранить"></Input>
            </div>
          </form>
        </DialogContent>
      </Dialog>

    </div>
  );
}