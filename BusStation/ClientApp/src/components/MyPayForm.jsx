import React, { useEffect } from 'react';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Autocomplete from '@material-ui/lab/Autocomplete';
import Divider from '@material-ui/core/Divider';
import { updateFlight } from '../API/FlightsApi';
import { getUser } from '../API/Auth';
import { updateBoughtTicket }  from '../API/BoughtTicketsApi'

export default function MyPayForm () {

    const [user, setUser] = React.useState(null);
    const _setUser = async () => {
        const response = await getUser();
        if (response.ok) {
            let body = await response.json();
            setUser(body);
        }
        else setUser(null);
    };
    useEffect(() => {
        _setUser();
    }, []);

    let currentTickets = JSON.parse(localStorage.getItem('tickets'));

    let sum = 0;
    currentTickets.map(t => sum += t.ticket.cost);
    console.log(sum);

    console.log(currentTickets);

    const pay = (e) => {
        e.preventDefault();
        currentTickets.map(t => {
            updateBoughtTicket({id: t.id, user: user, ticket: t.ticket, isPaid: true });
            updateFlight({id: t.ticket.flight.id, routeNumber: t.ticket.flight.route.number, arrivalTime: t.ticket.flight.arrivalTime,
            departureTime: t.ticket.flight.departureTime, seatsNumber:  t.ticket.flight.seatsNumber, busySeatsNumber: t.ticket.flight.busySeatsNumber + 1});
        });
        window.location.href = '/';
    }

    const payVariants = ["WebMoney", "QIWI", "Яндекс.Деньги"];
    return (
        <div align="center" style={{marginTop: '30px'}}>
            <Typography variant="h5" align="center">
                Оплата
            </Typography>
            <form style={{width: '75%', marginTop: '15px'}} onSubmit={pay}>
                <h5 style={{float: 'left'}}>К оплате:</h5>
                <h5 align="right">{sum} руб.</h5>
                <Divider></Divider>
                <Autocomplete style={{marginTop: '20px'}}
                    variant="outlined"
                    margin="normal"
                    options={payVariants}
                    getOptionLabel={(option) => option}
                    renderInput={(params) => <TextField {...params} label="Способ оплаты" variant="outlined" required  />}
                />
                <div style={{marginTop: '15px', display: 'flex', justifyContent: 'flex-start' }} align="left">
                    <Button
                        type="submit"
                        variant="contained"
                        color="primary">
                        Оплатить
                    </Button>
                    <Button variant="outlined" color="primary" style={{marginLeft: '15px'}}>
                        Отказаться от оплаты
                    </Button>
                </div>
            </form>
        </div>
    );
}