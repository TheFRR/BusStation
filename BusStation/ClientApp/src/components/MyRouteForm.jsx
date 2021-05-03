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
      <div style={{ margin: '0 auto' }}>
      <TextField
          id="outlined-password-input"
          label="Номер маршрута"
          variant="outlined"
        />
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
        <MySaveButton></MySaveButton>
      </div>
    </form>
  );
}