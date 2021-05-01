import React from 'react';
import TextField from '@material-ui/core/TextField';
import { makeStyles } from '@material-ui/core/styles';
import MySaveButton from "../components/MySaveButton.jsx"

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

  return (
    <form className={classes.root} noValidate autoComplete="off">
      <div style={{ width: '80%', margin: 'auto' }}>
        <TextField
          id="outlined-required"
          label="Пункт отправки"
          variant="outlined"
        />
        <TextField
          id="outlined-disabled"
          label="Пункт прибытия"
          variant="outlined"
        />
        <TextField
          id="outlined-password-input"
          label="Время в пути (мин)"
          variant="outlined"
        />
        <MySaveButton></MySaveButton>
      </div>
    </form>
  );
}