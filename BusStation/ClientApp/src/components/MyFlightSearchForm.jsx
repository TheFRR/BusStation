import React, { useState, useEffect } from 'react';
import 'date-fns';
import TextField from '@material-ui/core/TextField';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { getRoutes } from '../API/RoutesApi';
import { useHistory } from "react-router-dom";

export default function FormPropsTextFields() {

  const [departure, setDeparture] = React.useState(localStorage.getItem('form') !== null ? JSON.parse(localStorage.getItem('form')).departure : "");
  const [arrival, setArrival] = React.useState(localStorage.getItem('form') !== null ? JSON.parse(localStorage.getItem('form')).arrival : "");
  const [arrivalTime, setArrivalTime] = React.useState(localStorage.getItem('form') !== null ? JSON.parse(localStorage.getItem('form')).arrivalTime : "");

  const [routes, setRoutes] = useState([]);
  const _getRoutes = async () => {
    let temp = await getRoutes();
    setRoutes(temp);
  };

  useEffect(() => {
    _getRoutes();
  }, []);

  const history = useHistory();
  const navigateToBuy = () => {
    history.push('/buy');
  }

  const handleSubmit = (e) => {
    e.preventDefault();
    navigateToBuy();
  };

  let departureRows = [];
  routes.map(r => { if (!departureRows.includes(r.departure)) departureRows.push(r.departure) });

  let arrivalRows = [];
  routes.map(r => { if (!arrivalRows.includes(r.arrival)) arrivalRows.push(r.arrival) });

  localStorage.setItem("form", JSON.stringify( {departure: departure, arrival: arrival, arrivalTime: arrivalTime} ));

  return (
    <form autoComplete="off" style={{  width: '300px', margin: '0 auto', 
    backgroundColor: 'lightsteelblue', paddingTop: '15px', paddingBottom: '15px' }} onSubmit={handleSubmit}>
        <h5 style={{textAlign: 'center'}}>Купить билет на автобус</h5>
        <Autocomplete options={departureRows} value={departure}
          getOptionLabel={(option) => option}
          renderInput={(params) => <TextField {...params} label="Пункт отправления" variant="outlined" required  />}
            style={{width: '100%', paddingBottom: '15px', paddingTop: '10px'}}
            onSelect={(e) => setDeparture(e.target.value)}/>
        <Autocomplete required options={arrivalRows} value={arrival} 
            getOptionLabel={(option) => option}
            renderInput={(params) => <TextField {...params} label="Пункт отправления" variant="outlined" required />}
            style={{width: '100%', paddingBottom: '15px', paddingTop: '10px'}}
            onSelect={(e) => setArrival(e.target.value)}/>
        <TextField 
            required type="datetime-local" label="Время прибытия (+/- 1 час)" variant="outlined" 
            InputLabelProps={{ shrink: true }} style={{width: '100%', paddingBottom: '15px', marginTop: '10px'}}
            value={arrivalTime} onChange={(e) => setArrivalTime(e.target.value)}></TextField>
        <TextField type="submit" value="Найти" color="primary"
        variant="outlined" size="small" style={{ display: 'flex', marginTop:'5px' }}></TextField>
    </form>
  );
}
