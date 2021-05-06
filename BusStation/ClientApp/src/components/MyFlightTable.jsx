import React, { useState, useEffect } from 'react';
import Button from '@material-ui/core/Button';
import Input from '@material-ui/core/Input';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { DataGrid } from '@material-ui/data-grid';
import { getFlights } from '../API/FlightsApi';
import { deleteFlight } from '../API/FlightsApi';
import { updateFlight } from '../API/FlightsApi';
import { useHistory } from "react-router-dom";
import * as moment from 'moment';

const columns = [
  { field: 'routeNumber', headerName: 'Номер маршрута', width: 200, type: 'string' },
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

  const history = useHistory();
  const navigateToRoutes = () => {
    history.push('/route');
  }

  const [open, setOpen] = React.useState(false);

  const [id, setId] = React.useState('');
  const [route, setRoute] = React.useState('');
  const [seats, setSeats] = React.useState('');
  const [busySeats, setBusySeats] = React.useState(0);
  const [departureTime, setDeparture] = React.useState('');
  const [arrivalTime, setArrival] = React.useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    updateFlight({ id: id, routeNumber: route, arrivalTime: arrivalTime.toString(), departureTime: departureTime.toString(), seatsNumber: seats, 
      busySeatsNumber: busySeats });
    window.location.reload();
  };

  const handleClickOpen = () => {
    setOpen(true);
    setId(deletedRows[deletedRows.length - 1].id);
    setRoute(deletedRows[deletedRows.length - 1].routeNumber);
    setSeats(deletedRows[deletedRows.length - 1].seats);
    setBusySeats(deletedRows[deletedRows.length - 1].busySeats);
    setDeparture(moment(deletedRows[deletedRows.length - 1].departureTime).format('YYYY-MM-DDTHH:mm'));
    setArrival(moment(deletedRows[deletedRows.length - 1].arrivalTime).format('YYYY-MM-DDTHH:mm'));
    console.log(departureTime);
  };

  const handleClose = () => {
    setOpen(false);
  };

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
  flights.map(f => rows.push({
    id: f.id, routeNumber: f.route.number, arrivalTime: new Date(f.arrivalTime), departureTime: new Date(f.departureTime),
    busySeats: f.busySeatsNumber, seats: f.seatsNumber
  }));

  return (
    <div style={{ height: 380, width: '80%', margin: 'auto' }}>
      <DataGrid rows={rows} columns={columns} pageSize={5}
        onRowSelected={handleRowSelection}></DataGrid>
      <div style={{ width: '80%', marginTop: '5px' }}>
        <Button color="primary" onClick={handleClickOpen}>Редактировать</Button>
        <Button color="primary" onClick={handlePurge}>Удалить</Button>
        <Button onClick={navigateToRoutes}>К списку маршрутов</Button>
      </div>

      <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
        <DialogTitle align="center">Редактирование рейса</DialogTitle>
        <DialogContentText align="center" style={{ paddingLeft: '10px', paddingRight: '10px' }}>
          Let Google help apps determine location. This means sending anonymous location data to
          Google, even when no apps are running.
          </DialogContentText>
        <DialogContent>
          <form onSubmit={handleSubmit}>
            <div style={{ marginBottom: '15px' }}>
              <TextField
                id="departure"
                type="datetime-local"
                //KeyboardButtonProps={{ 'aria-label': 'change date', }}
                label="Время прибытия"
                InputLabelProps={{ shrink: true }}
                fullWidth
                variant="outlined"
                value={departureTime}
                onChange={(e) => setDeparture(e.target.value)}
              />
            </div>
            <div style={{ marginBottom: '15px' }}>
              <TextField
                id="arrival"
                type="datetime-local"
                label="Время прибытия"
                InputLabelProps={{ shrink: true }}
                fullWidth
                variant="outlined"
                value={arrivalTime}
                onChange={(e) => setArrival(e.target.value)}
              />
            </div>
            <div style={{ marginBottom: '15px' }}>
              <TextField
                id="seats"
                type="number"
                label="Всего мест"
                fullWidth
                variant="outlined"
                value={seats}
                onChange={(e) => setSeats(e.target.value)}
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