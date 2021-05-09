import React, { useState, useEffect } from 'react';
import MyFlightSearchForm from '../components/MyFlightSearchForm';
import MyTicketCard from '../components/MyTicketCard';
import MyNewsList from '../components/MyNewsList';
import Grid from '@material-ui/core/Grid';
import { getTickets } from '../API/TicketsApi';

import "../custom.css"

export const BuyTicket = () => {
    const [tickets, setTickets] = useState([]);
    const _getTickets = async () => {
        let temp = await getTickets();
        setTickets(temp);
    };
    useEffect(() => {
        _getTickets();
    }, []);

    let searchData = JSON.parse(localStorage.getItem("form"));

    let arrivalTimeForm = new Date(searchData.arrivalTime).getTime();
    let arrivalTimeFormPlus = new Date(arrivalTimeForm + 60 * 60 * 1000).getTime();
    let arrivalTimeFormMinus = new Date(arrivalTimeForm - 60 * 60 * 1000).getTime();

    let selectedTickets = [];  

    tickets.map(t => {
        let arrivalTimeTicket = new Date(t.flight.arrivalTime).getTime();
        if (arrivalTimeTicket <= arrivalTimeFormPlus && arrivalTimeTicket >= arrivalTimeFormMinus &&
        t.flight.route.arrival === searchData.arrival && t.flight.route.departure === searchData.departure &&
        t.flight.busySeatsNumber < t.flight.seatsNumber) selectedTickets.push(t);
    });
    
    return (
        <div>
            <div style={{ backgroundColor: 'lightsteelblue' }}>
                <MyFlightSearchForm></MyFlightSearchForm>
            </div>
            {   selectedTickets.length > 0 &&
                <Grid container style={{ display: 'flex', justifyContent: 'center' }}>
                <div id="divForTickets" style={{ flexGrow: 1, maxWidth: 750, marginBottom: '10px' }}>
                {
                    selectedTickets.map(t => (<MyTicketCard key={t.id} data={t}></MyTicketCard>))
                }
                </div>
                <MyNewsList></MyNewsList>
                </Grid>
            }
            {
                selectedTickets.length === 0 &&
                <Grid container style={{ display: 'flex', justifyContent: 'center' }}>
                <div id="divForTickets" align="center" style={{ flexGrow: 1, maxWidth: 750, marginBottom: '80px', paddingBottom: '40px' }}>
                    <h5 style={{marginTop: '30px'}}>Ничего не найдено...</h5>
                    <a>Попробуйте найти другой рейс.</a>
                </div>
                <MyNewsList></MyNewsList>
                </Grid>
            }
        </div>
    );
}