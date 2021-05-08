import React, { useState, useEffect } from 'react';
import Input from '@material-ui/core/TextField';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { makeStyles } from '@material-ui/core/styles';
import { insertFlight } from '../API/FlightsApi';
import { getRoutes } from '../API/RoutesApi';
import { insertTicket } from '../API/RoutesApi';

import "../custom.css"
import TextField from '@material-ui/core/TextField';

const useStyles = makeStyles((theme) => ({
  root: {
    '& .MuiTextField-root': {
      margin: theme.spacing(1),
      width: '30ch',
    },
  },
}));

export default function FormPropsTextFields() {
  const classes = useStyles();

  const [routes, setRoutes] = useState([]);
  const myMethod = async () => {
    let temp = await getRoutes();
    setRoutes(temp);
  };

  useEffect(() => {
    myMethod();
  }, []);

  const [number, setNumber] = React.useState();
  const [departure, setDeparture] = React.useState("");
  const [arrival, setArrival] = React.useState("");
  const [seats, setSeats] = React.useState(25);
  const [busySeats, setBusySeats] = React.useState(0);

  const handleSubmit = (e) => {
    e.preventDefault();  
    insertFlight({ routeNumber: number, arrivalTime: arrival, departureTime: departure, seatsNumber: seats, busySeatsNumber: busySeats });
    window.location.reload();
  };

  let routesList = [];
  routes.map(r => routesList.push({number: r.number}));

  return (
    <form className={classes.root} autoComplete="off" align="center" onSubmit={handleSubmit} style={{ margin: '30px auto' }}>
      <h3 align="center">Добавление рейса</h3>
      <div style={{ margin: '0 auto' }}>
        <Autocomplete
          id="number"
          options={routesList}
          getOptionLabel={(option) => option.number.toString()}
          renderInput={(params) => <TextField {...params} label="Номер маршрута" variant="outlined" />}
          onSelect={(e) => setNumber(e.target.value)}
        />
        <TextField
          id="departure"
          type="datetime-local"
          required
          label="Время отправки"
          variant="outlined"
          InputLabelProps={{ shrink: true }}
          value={departure}
          onChange={(e) => setDeparture(e.target.value)}
        />
        <TextField
          id="arrival"
          type="datetime-local"
          required
          label="Время прибытия"
          InputLabelProps={{ shrink: true }}
          variant="outlined"
          value={arrival}
          onChange={(e) => setArrival(e.target.value)}
        />
        <Input
          id="seatsNumber"
          type="number"
          required
          label="Кол-во мест"
          variant="outlined"
          value={seats}
          onChange={(e) => setSeats(e.target.value)}
        />
        <div><Input type="submit" value="Добавить" variant="outlined" size="small"></Input></div>
      </div>
    </form>
  );
}