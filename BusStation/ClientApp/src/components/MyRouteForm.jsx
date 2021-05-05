import React from 'react';
import Input from '@material-ui/core/TextField';
import { makeStyles } from '@material-ui/core/styles';
import { insertRoute } from '../API/RoutesApi'

import "../custom.css"

const useStyles = makeStyles((theme) => ({
  root: {
    '& .MuiTextField-root': {
      margin: theme.spacing(1),
      width: '25ch',
    },
  },
}));

export default function FormPropsTextFields() {
  const classes = useStyles();

  const [number, setNumber] = React.useState('');
  const [departure, setDeparture] = React.useState('');
  const [arrival, setArrival] = React.useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    insertRoute({number, departure, arrival});
    window.location.reload();
  };

  return (
    <form className={classes.root} noValidate autoComplete="off" onSubmit={handleSubmit}>
      <div style={{ margin: '0 auto' }}>
        <Input
          id="number"
          type="number"
          InputProps={{ inputProps: { min: 1 } }}
          label="Номер маршрута"
          variant="outlined"
          value={number}
          onChange={(e) => setNumber(e.target.value)}
        />
        <Input
          id="departure"
          label="Пункт отправки"
          variant="outlined"
          value={departure}
          onChange={(e) => setDeparture(e.target.value)}
        />
        <Input
          id="arrival"
          label="Пункт прибытия"
          variant="outlined"
          value={arrival}
          onChange={(e) => setArrival(e.target.value)}
        />
        <div><Input type="submit" value="Сохранить" variant="outlined" size="small"></Input></div>
      </div>
    </form>
  );
}