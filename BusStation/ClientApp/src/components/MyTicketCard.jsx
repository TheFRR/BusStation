import React, { useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { insertBoughtTicket } from '../API/BoughtTicketsApi';
import { getUser } from '../API/Auth';
import { useHistory } from "react-router-dom";
import * as moment from 'moment';

const useStyles = makeStyles({
    root: {
        width: 250,
        margin: '35px auto'
    },
    text: {
        display: 'inline-block',
        marginBottom: '5px',
    },
    title: {
        fontSize: 14,
        marginBottom: '1px',
    },
    content: {
        backgroundColor: 'lightsteelblue'
    }
});

export default function SimpleCard(props) {
    const classes = useStyles();

    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
      setOpen(true);
    };
  
    const handleClose = () => {
      setOpen(false);
    };

    const handleAccept = () => {
        insertBoughtTicket({ user: user, ticket: props.data, isPaid: false });
        // updateFlight({
        //     id: props.data.flight.id, routeNumber: props.data.flight.route.number,
        //     arrivalTime: props.data.flight.arrivalTime.toString(), departureTime: props.data.flight.departureTime.toString(),
        //     seatsNumber: props.data.flight.seatsNumber, busySeatsNumber: props.data.flight.busySeatsNumber + 1
        // });
        window.location.reload();
    }

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

    const addToBasket = () => {
        handleClickOpen();
    }

    const history = useHistory();

    const navigateToPay = () => {
        localStorage.setItem("ticket", JSON.stringify(props.data));
        history.push('/pay');
    }

    return (
        <div>
            <Card className={classes.root}>
                <CardContent className={classes.content}>
                    <Typography className={classes.title} color="textSecondary" gutterBottom>
                        Время отправления
                    </Typography>
                    <Typography className={classes.text} variant="h5" component="h2">
                        {moment(props.data.flight.departureTime).format('DD.MM.YYYY HH:mm')}
                    </Typography>
                    <Typography className={classes.title} color="textSecondary" gutterBottom>
                        Время прибытия
                    </Typography>
                    <Typography className={classes.text} variant="h5" component="h2">
                        {moment(props.data.flight.arrivalTime).format('DD.MM.YYYY HH:mm')}
                    </Typography>
                    <Typography className={classes.title} color="textSecondary" gutterBottom>
                        Цена (руб.)
                    </Typography>
                    <Typography variant="h5" component="h2">
                        {props.data.cost}
                    </Typography>
                </CardContent>
                { window.location.href === 'http://localhost:52646/buy' && 
                    <CardActions>
                        <Button size="small" onClick={addToBasket}>Добавить в корзину</Button>
                    </CardActions>
                }
            </Card>

            <Dialog
                open={open}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description">
                <DialogTitle id="alert-dialog-title">{"Оповещение"}</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        { user != null && <a>Данным действием вы добавляете билет в корзину. Продолжить?</a> }
                        { user == null && <a>Билеты могут приобретать только зарегестрированные пользователи. Пожлуйста, войдите в аккаунт или зарегиструйтесь.</a>}
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    { user != null && 
                    <div>
                        <Button onClick={handleAccept} color="primary">
                            ОК
                        </Button>
                        <Button onClick={handleClose} color="primary">
                            Отмена
                        </Button>
                    </div>
                    }
                    { user == null &&
                        <Button onClick={handleClose} color="primary">ОК</Button> 
                    }
                </DialogActions>
            </Dialog>
        </div>
    );
}