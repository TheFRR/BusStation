import React, { useState, useEffect } from 'react';
import MyTicketCard from '../components/MyTicketCard';
import Button from '@material-ui/core/Button';
import { getBoughtTicket } from '../API/BoughtTicketsApi';
import { getUser } from '../API/Auth';
import { useHistory } from "react-router-dom";

import "../custom.css"

export const Basket = () => {

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

    const [boughtTickets, setBoughtTickets] = useState([]);
    const _getBoughtTickets = async () => {
        let temp = await getBoughtTicket();
        setBoughtTickets(temp);
    };
    useEffect(() => {
        _getBoughtTickets();
    }, []);

    let selectedTickets = [];
    boughtTickets.map(t => { if (t.user.id === user.id && t.isPaid === false) selectedTickets.push(t)});
    console.log(selectedTickets);

    localStorage.setItem("tickets", JSON.stringify(selectedTickets));

    const history = useHistory();
    const navigateToPay = () => {
        history.push('/pay');
    }

    return (
        <div>
            { selectedTickets.map(t => 
                (<div>
                    <h5 align="center" style={{marginTop: '30px', marginBottom: '-20px'}}>Рейс «{t.ticket.flight.route.departure} – {t.ticket.flight.route.arrival}»</h5>
                    <MyTicketCard key={t.id} data={t.ticket}></MyTicketCard>
                </div>)
                ) 
            }
            { selectedTickets.length !== 0 &&
                <div align="center"><Button variant="contained" color="primary" 
                style={{width: '250px'}} onClick={navigateToPay}>Оплатить</Button></div>
            }
            { selectedTickets.length === 0 &&
                <div align="center" style={{marginTop: '30px'}}>
                    <h4>Ваша корзина пуста</h4>
                </div>
            }
        </div>
    );
}