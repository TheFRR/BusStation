import React from 'react';
import MyFlightSearchForm from '../components/MyFlightSearchForm.jsx'
import MyTicketCard from '../components/MyTicketCard'
import MyNewsList from '../components/MyNewsList'
import Grid from '@material-ui/core/Grid';

import "../custom.css"

export const BuyTicket = () => {
    return (
        <div>
            <div style={{ backgroundColor: 'lightsteelblue' }}>
                <MyFlightSearchForm></MyFlightSearchForm>
            </div>
            <Grid container style={{ display: 'flex', justifyContent: 'center' }}>
                <div style={{ flexGrow: 1, maxWidth: 750 }}>
                    <MyTicketCard></MyTicketCard>
                    <MyTicketCard></MyTicketCard>
                </div>
                <MyNewsList></MyNewsList>
            </Grid>
        </div>
    );
}