import React, { useState, useEffect } from 'react';
import ReactDom from 'react-dom';
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
    
    console.log(tickets);

    //tickets.map(t => { const elem = <MyTicketCard></MyTicketCard>; ReactDom.render(elem, document.getElementById('divForTickets')) });

    return (
        <div>
            <div style={{ backgroundColor: 'lightsteelblue' }}>
                <MyFlightSearchForm></MyFlightSearchForm>
            </div>
            <Grid container style={{ display: 'flex', justifyContent: 'center' }}>
                <div id="divForTickets" style={{ flexGrow: 1, maxWidth: 750, marginBottom: '80px' }}></div>
                <MyNewsList></MyNewsList>
            </Grid>
        </div>
    );
}