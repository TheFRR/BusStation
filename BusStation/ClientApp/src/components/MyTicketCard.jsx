import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Grid from '@material-ui/core/Grid';

const useStyles = makeStyles({
    root: {
        width: 300,
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

export default function SimpleCard() {
    const classes = useStyles();

    return (
        <Card className={classes.root}>
            <CardContent className={classes.content}>
                <Typography className={classes.title} color="textSecondary" gutterBottom>
                    Время отправления
                    </Typography>
                <Typography className={classes.text} variant="h5" component="h2">
                    00:00
                    </Typography>
                <Typography className={classes.title} color="textSecondary" gutterBottom>
                    Время прибытия
                    </Typography>
                <Typography className={classes.text} variant="h5" component="h2">
                    01:10
                    </Typography>
                <Typography className={classes.title} color="textSecondary" gutterBottom>
                    Цена (руб.)
                    </Typography>
                <Typography variant="h5" component="h2">
                    250
                    </Typography>
            </CardContent>
            <CardActions>
                <Button size="small">Купить</Button>
            </CardActions>
        </Card>
    );
}