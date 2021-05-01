import 'date-fns';
import React from 'react';
import TextField from '@material-ui/core/TextField';
import { makeStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import DateFnsUtils from '@date-io/date-fns';
import {
    MuiPickersUtilsProvider,
    KeyboardDatePicker,
} from '@material-ui/pickers';

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
      <div style={{  width: '40%', margin: 'auto', backgroundColor: 'lightsteelblue', padding: '15px' }}>
        <h5 style={{marginLeft: '5px'}}>Купить билет на автобус</h5>
        <TextField label="Пункт отправления" type="search"/>
        <TextField label="Пункт прибытия" type="search"/>
        <MuiPickersUtilsProvider utils={DateFnsUtils}>
            <KeyboardDatePicker
                disableToolbar
                variant="inline"
                format="MM/dd/yyyy"
                margin="normal"
                id="date-picker-inline"
                label="Дата отправления"
                KeyboardButtonProps={{
                    'aria-label': 'change date',
                }}
            />
        </MuiPickersUtilsProvider>
        <Button size="medium"
        color="primary" 
        variant="contained" style={{display: 'flex', marginTop:'5px', marginLeft: '5px'}}>Найти</Button>
      </div>
    </form>
  );
}
