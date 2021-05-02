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
    <form noValidate autoComplete="off" style={{  width: '300px', margin: '0 auto', 
    backgroundColor: 'lightsteelblue', paddingTop: '15px', paddingBottom: '15px' }}>
        <h5 style={{textAlign: 'center'}}>Купить билет на автобус</h5>
        <TextField label="Пункт отправления" type="search" style={{width: '100%'}}/>
        <TextField label="Пункт прибытия" type="search" style={{width: '100%'}}/>
        <MuiPickersUtilsProvider utils={DateFnsUtils} >
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
                style={{width: '100%'}}
            />
        </MuiPickersUtilsProvider>
        <Button size="medium"
        color="primary" 
        variant="contained" style={{ display: 'flex', marginTop:'5px' }}>Найти</Button>
    </form>
  );
}
