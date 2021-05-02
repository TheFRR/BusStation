import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import { AccessTime, AddLocation, Phone, Report } from '@material-ui/icons';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    maxWidth: 750,
  },
  demo: {
    backgroundColor: theme.palette.background.paper,
  },
  title: {
    margin: theme.spacing(4, 0, 2),
  },
}));

export default function InteractiveList() {
  const classes = useStyles();

  return (
    <div className={classes.root}> 
      <Grid container>
        <Grid item style={{width: '300px', height: '500px', marginLeft: 'auto', marginRight: 'auto'}}>
          <Typography variant="h6" className={classes.title}>
            Полезная информация и новости
          </Typography>
          <div className={classes.demo}>
            <List>
                <Button 
                variant="contained"
                color="primary"
                size="medium"
                style={{  width: '100%', margin: '10px auto' }}>История предприятия</Button>
                <Button 
                variant="contained"
                color="primary"
                size="medium"
                style={{  width: '100%', margin: '10px auto' }}>Информация о коронавирусе</Button>
                <ListItem>
                  <ListItemIcon>
                    <Report />
                  </ListItemIcon>
                  <ListItemText
                    primary="Внимание! Вторая станция закрыта на ремонт. Отправление автобусов временно происходит от первой станции"
                  />
                </ListItem>
                <ListItem>
                  <ListItemIcon>
                    <Report />
                  </ListItemIcon>
                  <ListItemText
                    primary="Рейсы до с. Жабное отменены"
                  />
                </ListItem>
            </List>
          </div>
        </Grid>
      </Grid>
    </div>
  );
}