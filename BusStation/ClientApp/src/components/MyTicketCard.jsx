import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
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

export default function SimpleCard (props) {
    const classes = useStyles();
    return (
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
            <CardActions>
                <Button size="small">Купить</Button>
            </CardActions>
        </Card>
    );
}