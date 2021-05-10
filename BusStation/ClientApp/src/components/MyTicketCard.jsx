import React, { useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Dialog from '@material-ui/core/Dialog';
import Box from '@material-ui/core/Box';
import Rating from '@material-ui/lab/Rating';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { getRatings } from '../API/RatingsApi';
import { insertRating } from '../API/RatingsApi';
import { insertBoughtTicket } from '../API/BoughtTicketsApi';
import { getUser } from '../API/Auth';
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
        window.location.reload();
    }

    const addToBasket = () => {
        handleClickOpen();
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

    const [value, setValue] = React.useState(0);

    const [ratings, setRatings] = React.useState(null);
    const _setRatings = async () => {
        let temp = await getRatings();
        setRatings(temp);
    };
    useEffect(() => {
        _setRatings();
    }, [value]);

    let selectedRatings = [];
    let userRating = 0;
    let ratingId = 0;
    if (ratings !== null) ratings.map(r => { 
        if (r.route.id === props.data.flight.route.id) selectedRatings.push(r);
        if (r.route.id === props.data.flight.route.id && r.user.id === user.id) 
        { 
            userRating = r.mark;  
            ratingId = r.id;
        }  
    });

    console.log(ratingId);

    useEffect(() => {
        setValue(userRating);
    }, [userRating]);

    let ratingSumm = 0;
    selectedRatings.map(r => ratingSumm += r.mark);
    ratingSumm /= selectedRatings.length;
    console.log(ratingSumm);

    return (
        <div>
            <Card className={classes.root}>
                <CardContent className={classes.content}>
                    <Typography className={classes.title} color="textSecondary" gutterBottom>
                        Маршрут
                    </Typography>
                    <Typography className={classes.text} variant="h5" component="h2">
                        №{props.data.flight.route.number}
                    </Typography>
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
                    <Typography className={classes.title} color="textSecondary" gutterBottom>Рейтинг маршрута</Typography>
                        <Typography variant="h5" component="h2">{isNaN(ratingSumm) ? 0.00 : ratingSumm.toFixed(2)}/5.00 </Typography>
                    { user !== null && user.userName !== "admin@mail.com" &&
                        <Box>
                            <Typography className={classes.title} color="textSecondary" gutterBottom>Ваша оценка</Typography>
                            <Rating 
                                readOnly={value === 0 ? false : true}
                                name="unique-rating"
                                value={value}
                                onChange={(event, newValue) => {
                                        setValue(newValue);
                                        _setRatings();
                                        insertRating({user: user, route: props.data.flight.route, mark: newValue});       
                                }}
                            />
                        </Box>
                    }
                </CardContent>
                { window.location.href === 'http://localhost:52646/buy' &&  user !== null && user.userName !== "admin@mail.com" &&
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
                <DialogTitle id="alert-dialog-title">{"Уведомление"}</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        { user != null && <a>Данным действием вы добавляете билет в корзину. Продолжить?</a> }
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